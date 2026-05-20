using BookRight.Domain.Interfaces;
using BookRight.UseCases.Commands.Booking.Status.Commands;

namespace BookRight.UseCases.Commands.Booking.Status.Handlers
{
    public class AfslutBookingHandler
    {
        private readonly IBookingStatusRepository _bookingStatusRepository;

        public AfslutBookingHandler(IBookingStatusRepository bookingStatusRepository)
        {
            _bookingStatusRepository = bookingStatusRepository;
        }
        public async Task<bool> HandleAsync(AfslutBookingCommand command)
        {
            var booking = await _bookingStatusRepository.HentPåIdAsync(command.bookingId);

            if (booking == null)
                return false;

            booking.MarkerAfsluttet();

            await _bookingStatusRepository.OpdaterAsync(booking);

            return true;
        }
    }
}
