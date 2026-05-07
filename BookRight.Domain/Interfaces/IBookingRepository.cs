using BookRight.Domain.Aggregates;
using BookRight.Domain.Models;

namespace BookRight.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<bool> ErBehandlerLedigAsync(Guid behandlerId, DateTime startTid, DateTime slutTid);
        Task<int> HentAntalAktiveBookingerAsync(Guid klinikId, DateTime startTid, DateTime slutTid);
        Task<IEnumerable<KundehistorikPost>> HentKundehistorikAsync(Guid kundeId);
        Task AddAsync(Booking booking);
    }
}