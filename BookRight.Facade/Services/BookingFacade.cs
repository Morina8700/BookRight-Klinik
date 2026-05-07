using BookRight.Facade.Contracts.Bookinger;
using BookRight.Facade.Contracts.Kunder;
using BookRight.Facade.Interfaces;
using BookRight.Domain.Interfaces;
using BookRight.UseCases.Commands;
using BookRight.UseCases.Queries;

namespace BookRight.Facade.Services
{
    public class BookingFacade : IBookingFacade
    {
        private readonly OpretBookingHandler _opretBookingHandler;
        private readonly HentKundehistorikHandler _hentKundehistorikHandler;
        private readonly IKundeRepository _kundeRepository;
        private readonly IBehandlerRepository _behandlerRepository;
        private readonly IKlinikRepository _klinikRepository;
        private readonly IBehandlingstypeRepository _behandlingstypeRepository;

        public BookingFacade(
            OpretBookingHandler opretBookingHandler,
            HentKundehistorikHandler hentKundehistorikHandler,
            IKundeRepository kundeRepository,
            IBehandlerRepository behandlerRepository,
            IKlinikRepository klinikRepository,
            IBehandlingstypeRepository behandlingstypeRepository)
        {
            _opretBookingHandler = opretBookingHandler;
            _hentKundehistorikHandler = hentKundehistorikHandler;
            _kundeRepository = kundeRepository;
            _behandlerRepository = behandlerRepository;
            _klinikRepository = klinikRepository;
            _behandlingstypeRepository = behandlingstypeRepository;
        }

        public async Task<BookingResponse> OpretBookingAsync(OpretBookingRequest request)
        {
            var command = new OpretBookingCommand(
                request.KundeId,
                request.BehandlerId,
                request.KlinikId,
                request.BehandlingstypeId,
                request.StartTid,
                request.SlutTid,
                0,
                0,
                null,
                request.KampagneId
            );

            var result = await _opretBookingHandler.HandleAsync(command);

            return new BookingResponse
            {
                Success = result.Success,
                Message = result.Success
        ? "Booking oprettet."
        : "Booking kunne ikke oprettes – tjek om behandleren er ledig og har ledige rum.",
                PrisUdenRabat = result.PrisUdenRabat,
                PrisMedRabat = result.PrisMedRabat,
                AnvendtRabatType = result.AnvendtRabatType
            };
        }

        public async Task<IEnumerable<KundeDto>> HentAlleKunderAsync()
        {
            var kunder = await _kundeRepository.HentAlleAsync();
            return kunder.Select(k => new KundeDto
            {
                KundeId = k.KundeId,
                FuldeNavn = $"{k.Fornavn} {k.Efternavn}",
                Email = k.Email,
                Telefon = k.Telefon,
                LoyalitetsNiveau = k.loyalitetsNiveau.ToString()
            });
        }

        public async Task<IEnumerable<KundehistorikDto>> HentKundehistorikAsync(Guid kundeId)
        {
            var historik = await _hentKundehistorikHandler.HandleAsync(kundeId);
            return historik.Select(h => new KundehistorikDto
            {
                BookingId = h.BookingId,
                StartTid = h.StartTid,
                SlutTid = h.SlutTid,
                KlinikNavn = h.KlinikNavn,
                BehandlerNavn = h.BehandlerNavn,
                BehandlingstypeNavn = h.BehandlingstypeNavn,
                Status = h.Status.ToString()
            });
        }

        public async Task<IEnumerable<BehandlerDto>> HentAlleBehandlereAsync()
        {
            var behandlere = await _behandlerRepository.HentAlleAsync();
            return behandlere.Select(b => new BehandlerDto
            {
                BehandlerId = b.BehandlerId,
                FuldeNavn = $"{b.Fornavn} {b.Efternavn}",
                AutorisationsType = b.AutorisationsType.ToString(),
                Klinikker = b.Klinikker.Select(k => new KlinikDto
                {
                    KlinikId = k.KlinikId,
                    Navn = k.Navn,
                    Adresse = k.Adresse
                }),
                Behandlingstyper = b.Behandlingstyper.Select(bt => new BehandlingstypeDto
                {
                    BehandlingstypeId = bt.BehandlingstypeId,
                    Navn = bt.Navn,
                    Pris = bt.Pris,
                    VarighedMinutter = bt.VarighedMinutter
                })
            });
        }

        public async Task<IEnumerable<KlinikDto>> HentAlleKlinikkerAsync()
        {
            var klinikker = await _klinikRepository.HentAlleAsync();
            return klinikker.Select(k => new KlinikDto
            {
                KlinikId = k.KlinikId,
                Navn = k.Navn,
                Adresse = k.Adresse
            });
        }

        public async Task<IEnumerable<BehandlingstypeDto>> HentAlleBehandlingstyperAsync()
        {
            var typer = await _behandlingstypeRepository.HentAlleAsync();
            return typer.Select(bt => new BehandlingstypeDto
            {
                BehandlingstypeId = bt.BehandlingstypeId,
                Navn = bt.Navn,
                Pris = bt.Pris,
                VarighedMinutter = bt.VarighedMinutter
            });
        }
    }
}