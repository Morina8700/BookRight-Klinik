namespace BookRight.UseCases.Commands.Booking.OpretBooking
{
    // Input transporteres fra UI til OpretBookingHandler
    public record OpretBookingCommand(
        Guid KundeId,
        Guid BehandlerId,
        Guid KlinikId,
        Guid BehandlingstypeId,
        DateTime StartTid,
        DateTime SlutTid
    );
}
