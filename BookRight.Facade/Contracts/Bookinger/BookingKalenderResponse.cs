namespace BookRight.Facade.Contracts.Bookinger
{
    public record BookingKalenderResponse(
           Guid BookingId,
           string KundeNavn,
           string BehandlerNavn,
           string BehandlingstypeNavn,
           DateTime StartTid,
           DateTime SlutTid,
           string Status,
           decimal PrisUdenRabat,
           decimal PrisMedRabat,
           string AnvendtRabatType
       );
}
