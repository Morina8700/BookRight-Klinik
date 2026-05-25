using BookRight.Facade.Contracts.Bookinger;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Commands.Booking.OpretBooking;
using BookRight.UseCases.Commands.Booking.Status.Commands;
using BookRight.UseCases.Commands.Booking.Status.Handlers;
using BookRight.UseCases.Queries.Behandler;
using BookRight.UseCases.Queries.Behandler.Behandlingstype;
using BookRight.UseCases.Queries.Kalender;
using BookRight.UseCases.Queries.Klinik;
using BookRight.UseCases.Queries.Kundehistorik;

namespace BookRight.Facade.Services
{
    public class BookingFacade : IBookingFacade
    {
        private readonly OpretBookingHandler _opretBookingHandler;
        private readonly HentKundehistorikHandler _hentKundehistorikHandler;
        private readonly HentAlleKlinikkerHandler _hentAlleKlinikkerHandler;
        private readonly HentAlleBehandlereHandler _hentAlleBehandlereHandler;
        private readonly HentAlleBehandlingstyperHandler _hentAlleBehandlingstyperHandler;
        private readonly AflysBookingHandler _aflysBookingHandler;
        private readonly AnkommetBookingHandler _ankommetBookingHandler;
        private readonly AfslutBookingHandler _afslutBookingHandler;
        private readonly NoShowBookingHandler _noShowBookingHandler;
        private readonly HentBookingHandler _hentBookingHandler;

        public BookingFacade(
            OpretBookingHandler opretBookingHandler,
            HentKundehistorikHandler hentKundehistorikHandler,
            HentAlleKlinikkerHandler hentAlleKlinikkerHandler,
            HentAlleBehandlereHandler hentAlleBehandlereHandler,
            HentAlleBehandlingstyperHandler hentAlleBehandlingstyperHandler,
            AflysBookingHandler aflysBookingHandler,
            AnkommetBookingHandler ankommetBookingHandler,
            AfslutBookingHandler afslutBookingHandler,
            HentBookingHandler hentBookingHandler,
            NoShowBookingHandler noShowBookingHandler)
        {
            _opretBookingHandler = opretBookingHandler;
            _hentKundehistorikHandler = hentKundehistorikHandler;
            _hentAlleKlinikkerHandler = hentAlleKlinikkerHandler;
            _hentAlleBehandlereHandler = hentAlleBehandlereHandler;
            _hentAlleBehandlingstyperHandler = hentAlleBehandlingstyperHandler;
            _aflysBookingHandler = aflysBookingHandler;
            _ankommetBookingHandler = ankommetBookingHandler;
            _afslutBookingHandler = afslutBookingHandler;
            _noShowBookingHandler = noShowBookingHandler;
            _hentBookingHandler = hentBookingHandler;
        }

        public async Task<BookingResponse> OpretBookingAsync(OpretBookingRequest request)
        {
            // Mapper request fra UI/API til command, som vores use case kan arbejde med
            var command = new OpretBookingCommand(
                request.KundeId,
                request.BehandlerId,
                request.KlinikId,
                request.BehandlingstypeId,
                request.StartTid,
                request.SlutTid);

            // Sender commanden videre til handleren, som laver selve bookingen
            var result = await _opretBookingHandler.HandleAsync(command);

            // Mapper resultatet tilbage til et response, som UI/API kan bruge
            return new BookingResponse
            {
                Success = result.Success,
                PrisUdenRabat = result.PrisUdenRabat,
                PrisMedRabat = result.PrisMedRabat,
                AnvendtRabatType = result.AnvendtRabatType,
                RabatProcent = result.RabatProcent,
                Besked = "Booking oprettet"
            };
        }

        public async Task<IEnumerable<KlinikDto>> HentAlleKlinikkerAsync()
        {
            // Henter klinikker via use case og laver dem om til DTOs til dropdown
            var klinikker = await _hentAlleKlinikkerHandler.HandleAsync();

            return klinikker.Select(k => new KlinikDto
            {
                KlinikId = k.KlinikId,
                Navn = k.Navn,
                Adresse = k.Adresse,
                AntalRum = k.AntalRum
            });
        }

        public async Task<IEnumerable<BehandlerDto>> HentAlleBehandlereAsync()
        {
            // Henter behandlere via use case og laver dem om til DTOs til dropdown
            var behandlere = await _hentAlleBehandlereHandler.HandleAsync();

            return behandlere.Select(b => new BehandlerDto
            {
                BehandlerId = b.BehandlerId,
                Fornavn = b.Fornavn,
                Efternavn = b.Efternavn,
                // Gemmer id på de klinikker, hvor behandleren arbejder
                KlinikIds = b.KlinikIds.ToList(),
                // Gemmer id på de behandlingstyper, som behandleren må udføre
                BehandlingstypeIds = b.BehandlingstypeIds.ToList()
            });
        }

        public async Task<IEnumerable<BehandlingstypeDto>> HentAlleBehandlingstyperAsync()
        {
            // Henter behandlingstyper via use case og laver dem om til DTOs til dropdown
            var behandlingstyper = await _hentAlleBehandlingstyperHandler.HandleAsync();

            return behandlingstyper.Select(b => new BehandlingstypeDto
            {
                BehandlingstypeId = b.BehandlingstypeId,
                Navn = b.Navn,
                Pris = b.Pris,
                VarighedMinutter = b.VarighedMinutter
            });
        }

        public Task<bool> AflysBookingAsync(Guid bookingId)
        {
            return _aflysBookingHandler.HandleAsync(new AflysBookingCommand(bookingId));
        }

        public Task<bool> AfslutBookingAsync(Guid bookingId)
        {
            return _afslutBookingHandler.HandleAsync(new AfslutBookingCommand(bookingId));
        }

        public Task<bool> MarkerAnkommetAsync(Guid bookingId)
        {
            return _ankommetBookingHandler.HandleAsync(new MarkerAnkommetCommand(bookingId));
        }

        public Task<bool> MarkerNoShowAsync(Guid bookingId)
        {
            return _noShowBookingHandler.HandleAsync(new MarkerNoShowCommand(bookingId));
        }

        public async Task<List<BookingKalenderResponse>> HentBookingerForDatoAsync(DateOnly dato)
        {
            // Henter bookinger for dato via use case og mapper til response til UI
            var bookinger = await _hentBookingHandler.HandleAsync(
                new HentBookingerQuery(dato));

            return bookinger.Select(b => new BookingKalenderResponse(
                b.BookingId,
                b.KundeNavn,
                b.BehandlerNavn,
                b.BehandlingstypeNavn,
                b.StartTid,
                b.SlutTid,
                b.Status,
                b.PrisUdenRabat,
                b.PrisMedRabat,
                b.AnvendtRabatType
            )).ToList();
        }

        // Henter kundehistorik fra use case-laget og mapper den til DTOs, som UI kan vise
        public async Task<IEnumerable<KundehistorikDto>> HentKundehistorikAsync(Guid kundeId)
        {
            var historik = await _hentKundehistorikHandler.HandleAsync(kundeId);

            return historik.Select(h => new KundehistorikDto
            {
                BookingId = h.BookingId,
                StartTid = h.StartTid,
                SlutTid = h.SlutTid,
                BehandlingstypeNavn = h.BehandlingstypeNavn,
                BehandlerNavn = h.BehandlerNavn,
                KlinikNavn = h.KlinikNavn,
                Status = h.Status.ToString(),
                PrisMedRabat = h.PrisMedRabat,
                AnvendtRabatType = h.AnvendtRabatType
            });
        }
    }
}