using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Commands.BookingStatus.Commands
{
    // Commanden indeholder kun BookingId, fordi navnet AflysBookingCommand allerede beskriver handlingen.
    public record AflysBookingCommand(Guid bookingId);
}
