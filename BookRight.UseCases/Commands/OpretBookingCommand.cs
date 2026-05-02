namespace BookRight.UseCases.Commands
{
    public record OpretBookingCommand(
        Guid KundeId,
        Guid BehandlerId,
        Guid KlinikId,
        Guid BehandlingstypeId,
        DateTime StartTid,
        DateTime SlutTid,
        decimal PrisUdenRabat,
        decimal PrisMedRabat,
        string? AnvendtRabatType,
        Guid? KampagneId = null
    );
}