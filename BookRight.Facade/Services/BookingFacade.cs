using BookRight.Facade.Contracts.Bookinger;
using BookRight.Facade.Interfaces;
using BookRight.Domain.Interfaces;
using BookRight.UseCases.Commands;

namespace BookRight.Facade.Services
{
    public class BookingFacade : IBookingFacade
    {
        private readonly OpretBookingHandler _opretBookingHandler;
        private readonly IBehandlerRepository _behandlerRepository;
        private readonly IKlinikRepository _klinikRepository;
        private readonly IBehandlingstypeRepository _behandlingstypeRepository;

        public BookingFacade(
            OpretBookingHandler opretBookingHandler,
            IBehandlerRepository behandlerRepository,
            IKlinikRepository klinikRepository,
            IBehandlingstypeRepository behandlingstypeRepository)
        {
            _opretBookingHandler = opretBookingHandler;
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
                request.PrisUdenRabat,
                request.PrisMedRabat,
                request.AnvendtRabatType,
                request.KampagneId
            );

            var success = await _opretBookingHandler.HandleAsync(command);

            return new BookingResponse
            {
                Success = success,
                Message = success ? "Booking oprettet." : "Booking kunne ikke oprettes – tjek om behandleren er ledig og har ledige rum."
            };
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