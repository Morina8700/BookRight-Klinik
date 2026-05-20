using BookRight.Domain.Aggregates;

namespace BookRight.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<bool> ErBehandlerLedigAsync(Guid behandlerId, DateTime startTid, DateTime slutTid);
        Task<int> HentAntalOverlappendeBookingerAsync(Guid klinikId, DateTime startTid, DateTime slutTid);
        Task AddAsync(Booking booking);
    }
}