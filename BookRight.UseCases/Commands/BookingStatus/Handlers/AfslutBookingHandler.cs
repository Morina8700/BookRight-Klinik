using BookRight.Domain.Interfaces;
using BookRight.UseCases.Commands.BookingStatus.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Commands.BookingStatus.Handlers
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
            // Find den booking der skal afsluttes.
            var booking = await _bookingStatusRepository.HentPåIdAsync(command.bookingId);

            if (booking == null)
                return false;

            // Domain-metoden sikrer, at kun ankomne bookinger kan afsluttes.
            booking.MarkerAfsluttet();

            await _bookingStatusRepository.OpdaterAsync(booking);

            return true;
        }
    }
}
