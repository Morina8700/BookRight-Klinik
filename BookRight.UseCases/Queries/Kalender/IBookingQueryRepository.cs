namespace BookRight.UseCases.Queries.Kalender
{
    public interface IBookingQueryRepository
    {
        Task<List<BookingKalenderPost>> HentBookingerForDatoAsync(DateOnly dato);
    }
}
