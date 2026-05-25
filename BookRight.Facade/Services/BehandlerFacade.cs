using BookRight.Facade.Contracts.Behandler;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Commands;
using BookRight.Facade.Contracts.Bookinger;
using BookRight.UseCases.Queries.Behandler.Behandlingstype;
using BookRight.UseCases.Queries.Klinik;

namespace BookRight.Facade.Services
{
    public class BehandlerFacade : IBehandlerFacade
    {
        private readonly OpretBehandlerHandler _opretBehandlerHandler;
        private readonly HentAlleKlinikkerHandler _hentAlleKlinikkerHandler;
        private readonly HentAlleBehandlingstyperHandler _hentAlleBehandlingstyperHandler;

        public BehandlerFacade(
            OpretBehandlerHandler opretBehandlerHandler,
            HentAlleKlinikkerHandler hentAlleKlinikkerHandler,
            HentAlleBehandlingstyperHandler hentAlleBehandlingstyperHandler)
        {
            _opretBehandlerHandler = opretBehandlerHandler;
            _hentAlleKlinikkerHandler = hentAlleKlinikkerHandler;
            _hentAlleBehandlingstyperHandler = hentAlleBehandlingstyperHandler;
        }

        public async Task<Guid> OpretBehandlerAsync(OpretBehandlerRequest request)
        {
            var command = new OpretBehandlerCommand(
            Guid.NewGuid(),
            request.Fornavn,
            request.Efternavn,
            request.Email,
            request.Telefon,
            request.AutorisationsNummer,
            request.AutorisationsType.ToString(),
            request.KlinikIds,
            request.BehandlingstypeIds
            );

            return await _opretBehandlerHandler.HandleAsync(command);
        }

        public async Task<IEnumerable<KlinikDto>> HentAlleKlinikkerAsync()
        {
            var klinikker = await _hentAlleKlinikkerHandler.HandleAsync();

            return klinikker.Select(k => new KlinikDto
            {
                KlinikId = k.KlinikId,
                Navn = k.Navn,
                Adresse = k.Adresse,
                AntalRum = k.AntalRum
            });
        }

        public async Task<IEnumerable<BehandlingstypeDto>> HentAlleBehandlingstyperAsync()
        {
            var behandlingstyper = await _hentAlleBehandlingstyperHandler.HandleAsync();

            return behandlingstyper.Select(b => new BehandlingstypeDto
            {
                BehandlingstypeId = b.BehandlingstypeId,
                Navn = b.Navn,
                Pris = b.Pris,
                VarighedMinutter = b.VarighedMinutter,
                KrævetAutorisationsType = Enum.Parse<AutorisationsTypeDTO>(
    b.KrævetAutorisationsType)


            });
        }
    }
}
