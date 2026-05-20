using BookRight.Domain.Aggregates;
namespace BookRight.Domain.Interfaces
{
    public interface IKundeRepository
    {
        Task TilføjAsync(Kunde kunde);
        Task<Kunde?> HentPåIdAsync(Guid kundeID);
        Task<IEnumerable<Kunde>> HentAlleAsync();
    }
}
