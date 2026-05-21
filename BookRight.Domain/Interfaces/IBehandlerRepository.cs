using BookRight.Domain.Aggregates;

namespace BookRight.Domain.Interfaces
{
    public interface IBehandlerRepository
    {
        Task<Behandler?> HentEfterIdAsync(Guid behandlerId);
        Task<IEnumerable<Behandler>> HentAlleAsync();
        Task AddAsync(Behandler behandler);
    }
}