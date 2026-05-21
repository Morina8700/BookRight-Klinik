using BookRight.Facade.Contracts.Bookinger;

namespace BookRight.Facade.Interfaces
{
    public interface IKlinikFacade
    {
        Task<IEnumerable<KlinikDto>> HentAlleAsync();
    }
}
