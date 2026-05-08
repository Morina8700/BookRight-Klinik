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
            var kunde = await _kundeRepository.HentPåIdAsync(command.KundeId);

            if (kunde is null)
                return new OpretBookingResult { Success = false };

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

            // I/O-bound arbejde: kampagner hentes fra databasen før rabatberegningen starter.
            var bookingDato = DateOnly.FromDateTime(command.StartTid);
            var aktiveKampagner = await _kampagneRepository.HentAktiveKampagnerAsync(bookingDato);

            // Context samler alle oplysninger, som strategierne skal bruge.
            // Derfor skal rabatstrategierne ikke selv hente data fra databasen.
            var rabatContext = new RabatBeregningContext(
                PrisUdenRabat: new Penge(behandlingstype.Pris),
                BookingDato: DateOnly.FromDateTime(command.StartTid),
                KundeFoedselsdato: kunde.Fødselsdato,
                LoyalitetsNiveau: kunde.loyalitetsNiveau,
                FoedselsdagsrabatBrugt: false, // Der skal laves kunde historik for at kunne tjekke dette, så det sættes til false for nu
                Behandlingstyper: [MapTilBehandlingsType(behandlingstype)],
                AktivKampagner: aktiveKampagner
                );

            // CPU-bound arbejde: loyalitet, fødselsdag og kampagne beregnes parallelt i rabatservicen.
            var rabatResultat = await _rabatBeregner.BeregnBedsteRabatAsync(rabatContext);

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
