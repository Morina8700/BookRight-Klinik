using BookRight.Domain.Aggregates;

namespace BookRight.Domain.Interfaces
{
    public interface IKlinikRepository
    {
        Task<Klinik> HentAsync(Guid klinikId);
        Task<IEnumerable<Klinik>> HentAlleAsync();
    }
}