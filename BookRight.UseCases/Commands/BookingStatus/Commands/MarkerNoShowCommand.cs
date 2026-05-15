using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Commands.BookingStatus.Commands
{
    //Commanden bruges når kunden ikke møder op til en aktiv booking
    public record MarkerNoShowCommand(Guid bookingId);
}
