namespace BookRight.UseCases.Commands
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