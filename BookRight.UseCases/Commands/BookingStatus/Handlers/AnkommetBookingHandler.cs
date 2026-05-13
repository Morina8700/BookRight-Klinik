using BookRight.Domain.Interfaces;
using BookRight.UseCases.Commands.BookingStatus.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Commands.BookingStatus.Handlers
{
    public class AnkommetBookingHandler
    {
        private readonly IBookingStatusRepository _bookingStatusRepository;

        public AnkommetBookingHandler(IBookingStatusRepository bookingStatusRepository)
        {
            _bookingStatusRepository = bookingStatusRepository;
        }

        public async Task<bool> HandleAsync(MarkerAnkommetCommand command)
        {
            // Find den booking hvor kunden er mødt op.
            var booking = await _bookingStatusRepository.HentPåIdAsync(command.bookingId);

            if (booking == null)
                return false;

            // Domain-metoden sikrer, at kun aktive bookinger kan markeres som ankommet.
            booking.MarkerAnkommet();

            await _bookingStatusRepository.OpdaterAsync(booking);

            return true;
        }
    }
}
