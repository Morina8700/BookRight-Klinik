using System;
using System.Collections.Generic;
using System.Text;
using BookRight.Domain.Interfaces;
using BookRight.UseCases.Commands.BookingStatus.Commands;

namespace BookRight.UseCases.Commands.BookingStatus.Handlers
{
    public class AflysBookingHandler
    {
        private readonly IBookingStatusRepository _bookingStatusRepository;

        public AflysBookingHandler(IBookingStatusRepository bookingStatusRepository)
        {
            _bookingStatusRepository = bookingStatusRepository;
        }

        public async Task<bool> HandleAsync(AflysBookingCommand command)
        {
            // Handleren henter bookingen gennem et repository-interface, ikke direkte fra databasen.
            var booking = await _bookingStatusRepository.HentPåIdAsync(command.bookingId);

            if(booking == null)
                return false;

            // Selve forretningsreglen ligger i Booking-aggregatet.
            booking.MarkerAflys();

            // Efter statusændringen gemmes bookingen igen.
            await _bookingStatusRepository.OpdaterAsync(booking);

            return true;
        }
    }
}
