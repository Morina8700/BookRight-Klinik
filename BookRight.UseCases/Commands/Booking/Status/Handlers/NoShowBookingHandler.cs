using BookRight.Domain.Interfaces;
using BookRight.UseCases.Commands.Booking.Status.Commands;

namespace BookRight.UseCases.Commands.Booking.Status.Handlers
{
    public class NoShowBookingHandler
    {
        private readonly IBookingStatusRepository _bookingStatusRepository;

        public NoShowBookingHandler(IBookingStatusRepository bookingStatusRepository)
        {
            _bookingStatusRepository = bookingStatusRepository;
        }
        public async Task<bool> HandleAsync(MarkerNoShowCommand command)
        {
            var booking = await _bookingStatusRepository.HentPåIdAsync(command.bookingId);

            if (booking == null)
                return false;

            booking.MarkerNoShow();

            await _bookingStatusRepository.OpdaterAsync(booking);

            return true;
        }
    }
}
