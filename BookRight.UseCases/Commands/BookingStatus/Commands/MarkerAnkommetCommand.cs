using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Commands.BookingStatus.Commands
{
    // Commanden bruges når kunden er mødt op i klinikken.
    public record MarkerAnkommetCommand(Guid bookingId);
 
}
