using BookRight.Domain.Aggregates;
namespace BookRight.Domain.Interfaces
{
    public interface IKundeRepository
    {
        Task TilføjAsync(Kunde kunde);
        Task OpdaterAsync(Kunde kunde);
        Task<Kunde?> HentPåIdAsync(Guid kundeID);
        Task<IEnumerable<Kunde>> HentAlleAsync();
        Task<bool> EmailFindesAsync(string email);
        Task<bool> TelefonFindesAsync(string telefon);
    }
}
