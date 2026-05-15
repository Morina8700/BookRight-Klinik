using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.DTOs
{
    public record BookingKalenderDto(
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
