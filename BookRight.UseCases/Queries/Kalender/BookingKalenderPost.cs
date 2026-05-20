namespace BookRight.UseCases.Queries.Kalender
{
    public record BookingKalenderPost(
        Guid BookingId,
        string KundeNavn,
        string BehandlerNavn,
        string BehandlingstypeNavn,
        DateTime StartTid,
        DateTime SlutTid,
        string Status,
        decimal PrisUdenRabat,
        decimal PrisMedRabat,
        string? AnvendtRabatType
    );
}
