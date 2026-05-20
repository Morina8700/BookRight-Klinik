namespace BookRight.UseCases.Commands.Booking.Status.Commands
{
    //Commanden bruges når kunden ikke møder op til en aktiv booking
    public record MarkerNoShowCommand(Guid bookingId);
}
