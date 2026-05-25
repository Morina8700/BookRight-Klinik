using BookRight.Facade.Contracts.Behandler;
using BookRight.Facade.Contracts.Bookinger;

namespace BookRight.Facade.Interfaces
{
    public interface IBehandlerFacade
    {
        Task<Guid> OpretBehandlerAsync(OpretBehandlerRequest request);
        Task<IEnumerable<KlinikDto>> HentAlleKlinikkerAsync();
        Task<IEnumerable<BehandlingstypeDto>> HentAlleBehandlingstyperAsync();
    }
}
