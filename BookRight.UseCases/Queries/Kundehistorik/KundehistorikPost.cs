using BookRight.Domain.Enums;

namespace BookRight.UseCases.Queries.Kundehistorik;

public record KundehistorikPost(
    Guid BookingId,
    DateTime StartTid,
    DateTime SlutTid,
    string BehandlingstypeNavn,
    string BehandlerNavn,
    string KlinikNavn,
    BookingStatus Status,
    decimal PrisMedRabat,
    string? AnvendtRabatType
);