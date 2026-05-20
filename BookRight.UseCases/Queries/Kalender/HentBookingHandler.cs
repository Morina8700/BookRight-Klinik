namespace BookRight.UseCases.Queries.Kalender
{
    public class HentBookingHandler
    {
        private readonly IBookingQueryRepository _bookingQueryRepository;

        public HentBookingHandler(IBookingQueryRepository bookingQueryRepository)
        {
            _bookingQueryRepository = bookingQueryRepository;
        }

        public async Task<List<BookingKalenderPost>> HandleAsync(HentBookingerQuery query)
        {
            return await _bookingQueryRepository.HentBookingerForDatoAsync(query.Dato);
        }
    }
}
