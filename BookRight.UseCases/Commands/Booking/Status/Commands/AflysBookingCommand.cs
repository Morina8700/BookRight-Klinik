namespace BookRight.UseCases.Commands.Booking.Status.Commands
{
    // Commanden indeholder kun BookingId, fordi navnet AflysBookingCommand allerede beskriver handlingen.
    public record AflysBookingCommand(Guid bookingId);
}
