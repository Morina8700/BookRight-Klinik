using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.Interfaces;
using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Domain.ValueObjects;

namespace BookRight.UseCases.Commands
{
    public class OpretBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IKlinikRepository _klinikRepository;
        private readonly IBehandlerRepository _behandlerRepository;
        private readonly IBehandlingstypeRepository _behandlingstypeRepository;
        private readonly BedsteRabatBeregner _bedsteRabatBeregner;
        private readonly IKundeRepository _kundeRepository;

        public OpretBookingHandler(
            IBookingRepository bookingRepository,
            IKlinikRepository klinikRepository,
            IBehandlerRepository behandlerRepository,
            IBehandlingstypeRepository behandlingstypeRepository,
            BedsteRabatBeregner bedsteRabatBeregner,
            IKundeRepository kundeRepository
            )
        {
            _bookingRepository = bookingRepository;
            _kundeRepository = kundeRepository;
            _klinikRepository = klinikRepository;
            _behandlerRepository = behandlerRepository;
            _behandlingstypeRepository = behandlingstypeRepository;
            _bedsteRabatBeregner = bedsteRabatBeregner;
        }

        public async Task<OpretBookingResult> HandleAsync(OpretBookingCommand command)
        {
            var kunde = await _kundeRepository.HentPåIdAsync(command.KundeId);

            var behandler = await _behandlerRepository
                .HentMedDetaljerAsync(command.BehandlerId);

            var behandlingstype = await _behandlingstypeRepository
                .HentAsync(command.BehandlingstypeId);

            if (!behandler.ArbejderPå(command.KlinikId))
                return new OpretBookingResult { Success = false };

            if (!behandler.KanUdføre(behandlingstype))
                return new OpretBookingResult { Success = false };

            var behandlerLedig = await _bookingRepository
                .ErBehandlerLedigAsync(command.BehandlerId, command.StartTid, command.SlutTid);

            if (!behandlerLedig)
                return new OpretBookingResult { Success = false };

            var klinik = await _klinikRepository.HentAsync(command.KlinikId);
            var aktiveBookinger = await _bookingRepository
                .HentAntalAktiveBookingerAsync(command.KlinikId, command.StartTid, command.SlutTid);

            if (aktiveBookinger >= klinik.AntalRum)
                return new OpretBookingResult { Success = false };

            // Handleren henter først data fra databasen med async/await.
            // Derefter bygger den context-objektet og sender det til rabatberegneren.
            var rabatContext = new RabatBeregningContext(
                PrisUdenRabat: new Penge(behandlingstype.Pris),
                BookingDato: DateOnly.FromDateTime(command.StartTid),
                KundeFoedselsdato: kunde.Fødselsdato,
                LoyalitetsNiveau: kunde.loyalitetsNiveau,
                FoedselsdagsrabatBrugt: false, // Vi antager at fødselsdagsrabatten ikke er brugt,
                                               // da vi ikke har information om tidligere bookinger i denne handler.
                Behandlingstyper: [MapTilBehandlingsType(behandlingstype)],
                AktivKampagner: [] //Ikke brugt lige nu



                );
            

            var rabatResultat = _bedsteRabatBeregner.BeregnBedsteRabat(rabatContext);

            var booking = new Booking(
                command.KundeId,
                command.BehandlerId,
                command.KlinikId,
                command.BehandlingstypeId,
                command.StartTid,
                command.SlutTid,
                rabatResultat.PrisUdenRabat.Belob,
                rabatResultat.PrisMedRabat.Belob,
                rabatResultat.RabatType.ToString(),
                command.KampagneId
            );

            await _bookingRepository.AddAsync(booking);
            return new OpretBookingResult
            {
                Success = true,
                PrisUdenRabat = rabatResultat.PrisUdenRabat.Belob,
                PrisMedRabat = rabatResultat.PrisMedRabat.Belob,
                AnvendtRabatType = rabatResultat.RabatType.ToString()
            };
        }

        private static BehandlingsType MapTilBehandlingsType(Behandlingstype behandlingstype)
        {
            if (behandlingstype.Navn.Contains("Fysioterapi", StringComparison.OrdinalIgnoreCase))
                return BehandlingsType.Fysioterapi;

            if (behandlingstype.Navn.Contains("Sportsmassage", StringComparison.OrdinalIgnoreCase))
                return BehandlingsType.Sportsmassage;

            if (behandlingstype.Navn.Contains("Akupunktur", StringComparison.OrdinalIgnoreCase))
                return BehandlingsType.Akupunktur;

            if (behandlingstype.Navn.Contains("Kostvejledning", StringComparison.OrdinalIgnoreCase))
                return BehandlingsType.Kostvejledning;

            if (behandlingstype.Navn.Contains("Holdtræning", StringComparison.OrdinalIgnoreCase) ||
                behandlingstype.Navn.Contains("genoptræning", StringComparison.OrdinalIgnoreCase))
                return BehandlingsType.Holdtræning;

            throw new InvalidOperationException($"Ukendt behandlingstype: {behandlingstype.Navn}");
        }
    }
}