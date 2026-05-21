using BookRight.Domain.Aggregates;
namespace BookRight.Domain.Interfaces
{
    public interface IBookingStatusRepository
    {
        // Bruges af status-handlers til at hente den booking, der skal ændres.
        Task<Booking?> HentPåIdAsync(Guid bookingId);

        // Gemmer booking efter Domain-laget har ændret status.
        Task OpdaterAsync(Booking booking);
    }
}
