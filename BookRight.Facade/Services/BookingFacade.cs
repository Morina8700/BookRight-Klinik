using BookRight.Facade.Contracts.Bookinger;
using BookRight.Facade.Interfaces;
using BookRight.Domain.Interfaces;
using BookRight.UseCases.Commands;

namespace BookRight.Facade.Services
{
    public class BookingFacade : IBookingFacade
    {
        private readonly OpretBookingHandler _opretBookingHandler;
        private readonly IKlinikRepository _klinikRepository;
        private readonly IBehandlerRepository _behandlerRepository;
        private readonly IBehandlingstypeRepository _behandlingstypeRepository;

        public BookingFacade(OpretBookingHandler opretBookingHandler, IKlinikRepository klinikRepository, IBehandlerRepository behandlerRepository, IBehandlingstypeRepository behandlingstypeRepository) 
        {
            _opretBookingHandler = opretBookingHandler;
            _klinikRepository = klinikRepository;
            _behandlerRepository = behandlerRepository;
            _behandlingstypeRepository = behandlingstypeRepository;
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
                Besked = "Booking oprettet"
            };
        }

        public async Task<IEnumerable<KlinikDto>> HentAlleKlinikkerAsync()
        {
            // Henter klinikker fra databasen og laver dem om til DTOs til dropdown
            var klinikker = await _klinikRepository.HentAlleAsync();

            return klinikker.Select(k => new KlinikDto
            {
                KlinikId = k.KlinikId,
                Navn = k.Navn ?? string.Empty,
                Adresse = k.Adresse ?? string.Empty,
                AntalRum = k.AntalRum
            });
        }

        public async Task<IEnumerable<BehandlerDto>> HentAlleBehandlereAsync()
        {
            // Henter behandlere fra databasen og laver dem om til DTOs til dropdown
            var behandlere = await _behandlerRepository.HentAlleAsync();

            return behandlere.Select(b => new BehandlerDto
            {
                BehandlerId = b.BehandlerId,
                Fornavn = b.Fornavn ?? string.Empty,
                Efternavn = b.Efternavn ?? string.Empty,
                // Gemmer id på de klinikker, hvor behandleren arbejder
                KlinikIds = b.Klinikker.Select(k => k.KlinikId).ToList(),
                // Gemmer id på de behandlingstyper, som behandleren må udføre
                BehandlingstypeIds = b.Behandlingstyper.Select(bt => bt.BehandlingstypeId).ToList()
            });
        }

        public async Task<IEnumerable<BehandlingstypeDto>> HentAlleBehandlingstyperAsync()
        {
            // Henter behandlingstyper fra databasen og laver dem om til DTOs til dropdown
            var behandlingstyper = await _behandlingstypeRepository.HentAlleAsync();

            return behandlingstyper.Select(b => new BehandlingstypeDto
            {
                BehandlingstypeId = b.BehandlingstypeId,
                Navn = b.Navn ?? string.Empty,
                Pris = b.Pris,
                VarighedMinutter = b.VarighedMinutter
            });
        }
    }
}