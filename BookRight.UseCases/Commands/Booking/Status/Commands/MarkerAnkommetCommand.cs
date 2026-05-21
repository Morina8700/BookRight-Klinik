namespace BookRight.UseCases.Commands.Booking.Status.Commands
{
    // Commanden brugers når kunden er mødt op i klinikken.
    public record MarkerAnkommetCommand(Guid bookingId);
}
