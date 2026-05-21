using BookRight.Facade.Contracts.Kunder;

namespace BookRight.Facade.Interfaces
{
    public interface IKundeFacade
    {
        Task<Guid> OpretKundeAsync(OpretKundeRequest request);
        Task<IReadOnlyList<KundeDto>> HentAlleKunderAsync();
    }
}
