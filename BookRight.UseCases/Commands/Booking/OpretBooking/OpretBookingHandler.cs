using BookRight.Domain.Enums;
using BookRight.Domain.Interfaces;
using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Domain.ValueObjects;
using DomainBooking = BookRight.Domain.Aggregates.Booking;

namespace BookRight.UseCases.Commands.Booking.OpretBooking
{
    // Udfører forretningslogikken for at oprette en booking
    public class OpretBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IKlinikRepository _klinikRepository;
        private readonly IBehandlerRepository _behandlerRepository;
        private readonly IBehandlingstypeRepository _behandlingstypeRepository;
        private readonly RabatBeregnerService _rabatBeregner;
        private readonly IKundeRepository _kundeRepository;
        private readonly IKampagneRepository _kampagneRepository;

        public OpretBookingHandler(
            IBookingRepository bookingRepository,
            IKlinikRepository klinikRepository,
            IBehandlerRepository behandlerRepository,
            IBehandlingstypeRepository behandlingstypeRepository,
            RabatBeregnerService rabatBeregner,
            IKundeRepository kundeRepository,
            IKampagneRepository kampagneRepository
            )
        {
            _bookingRepository = bookingRepository;
            _kundeRepository = kundeRepository;
            _klinikRepository = klinikRepository;
            _behandlerRepository = behandlerRepository;
            _behandlingstypeRepository = behandlingstypeRepository;
            _rabatBeregner = rabatBeregner;
            _kampagneRepository = kampagneRepository;
        }

        public async Task<OpretBookingResult> HandleAsync(OpretBookingCommand command)
        {
            // 1. Hent alle nødvendige data fra repositories
            var klinik = await _klinikRepository.HentEfterIdAsync(command.KlinikId)
            ?? throw new InvalidOperationException("Klinik ikke fundet");

            var behandler = await _behandlerRepository.HentEfterIdAsync(command.BehandlerId)
            ?? throw new InvalidOperationException("Behandler ikke fundet");

            var behandlingstype = await _behandlingstypeRepository.HentEfterIdAsync(command.BehandlingstypeId)
            ?? throw new InvalidOperationException("Behandlingstype ikke fundet");

            var kunde = await _kundeRepository.HentPåIdAsync(command.KundeId)
            ?? throw new InvalidOperationException("Kunde ikke fundet");

            // 2. Valider at behandleren arbejder på den valgte klinik
            if (!behandler.ArbejderPå(command.KlinikId))
                throw new InvalidOperationException("Behandler arbejder ikke på den valgte klinik");

            // 3. Valider at behandleren er autoriseret til behandlingstypen
            if (!behandler.KanUdføre(behandlingstype))
                throw new InvalidOperationException("Behandler er ikke autoriseret til behandlingstypen");

            // 4. Valider at behandleren ikke er dobbeltbooket
            bool erLedig = await _bookingRepository.ErBehandlerLedigAsync(command.BehandlerId, command.StartTid, command.SlutTid);
            if (!erLedig) throw new InvalidOperationException("Behandler er allerede booket i dette tidsrum");

            // 5. Valider at klinikken har ledige rum
            var aktiveBookinger = await _bookingRepository.HentAntalOverlappendeBookingerAsync(
            command.KlinikId, command.StartTid, command.SlutTid);
            if (!klinik.HarLedigeRum(aktiveBookinger))
                throw new InvalidOperationException("Klinikken har ingen ledige rum i det valgte tidsrum");

            // 6. Hent aktive kampagner og beregn bedste rabat (CPU-bound parallelisme)
            var aktiveKampagner = await _kampagneRepository.HentAktiveKampagnerAsync(DateOnly.FromDateTime(command.StartTid));
            var rabatBeregningContext = new RabatBeregningContext(
                new Penge(behandlingstype.Pris),
                DateOnly.FromDateTime(command.StartTid),
                kunde.Fødselsdato,
                kunde.loyalitetsNiveau,
                kunde.FoedselsdagsrabatBrugt,
                new List<BehandlingsType> { behandlingstype.Type },
                aktiveKampagner);
            var rabatResultat = await _rabatBeregner.BeregnBedsteRabatAsync(rabatBeregningContext);

            if (rabatResultat.RabatType == RabatType.Fødselsdag)
            {
                kunde.MarkerFoedselsdagsrabatBrugt();
                await _kundeRepository.OpdaterAsync(kunde);
            }
            
            // 7. Opret booking
            var booking = new DomainBooking(
                command.KundeId,
                command.BehandlerId,
                command.KlinikId,
                command.BehandlingstypeId,
                command.StartTid,
                command.SlutTid,
                rabatResultat.PrisUdenRabat.Belob,
                rabatResultat.PrisMedRabat.Belob,
                rabatResultat.RabatType.ToString());

            // 8. Gem booking
            await _bookingRepository.AddAsync(booking);

            // 9. Returner resultat til Facade
            return new OpretBookingResult
            {
                Success = true,
                PrisUdenRabat = rabatResultat.PrisUdenRabat.Belob,
                PrisMedRabat = rabatResultat.PrisMedRabat.Belob,
                RabatProcent = rabatResultat.RabatProcent.Value,
                AnvendtRabatType = rabatResultat.RabatType.ToString()
            };
        }
    }
}
