using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Commands.BookingStatus.Commands
{
    // Commanden bruges når receptionisten markerer en ankommet booking som afsluttet.
    public record AfslutBookingCommand(Guid bookingId);

}
