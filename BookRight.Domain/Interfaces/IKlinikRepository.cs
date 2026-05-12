using BookRight.Domain.Aggregates;

namespace BookRight.Domain.Interfaces
{
    public interface IKlinikRepository
    {
        Task<Klinik?> HentEfterIdAsync(Guid klinikId);
        Task<IEnumerable<Klinik>> HentAlleAsync();
    }
}