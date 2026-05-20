namespace BookRight.UseCases.Commands.Booking.Status.Commands
{
    // Commanden bruges når receptionisten markerer en ankommet booking som afsluttet.
    public record AfslutBookingCommand(Guid bookingId);
}
