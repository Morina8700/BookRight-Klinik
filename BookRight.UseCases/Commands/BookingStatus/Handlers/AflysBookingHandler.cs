using BookRight.Domain.Interfaces;
using BookRight.UseCases.Commands.BookingStatus.Commands;
using System;
using System.Collections.Generic;
using System.Text;

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
            var booking = await _bookingStatusRepository.HentPåIdAsync(command.bookingId);

            if (booking == null)
                return false;

            booking.Aflys();

            await _bookingStatusRepository.OpdaterAsync(booking);

            return true;
        }
    }
}
