using BookRight.Domain.Enums;

namespace BookRight.Domain.Models
{
    public class KundehistorikPost
    {
        public Guid BookingId { get; set; }
        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }
        public string BehandlingstypeNavn { get; set; } = string.Empty;
        public string BehandlerNavn { get; set; } = string.Empty; 
        public string KlinikNavn { get; set; } = string.Empty; 
        public BookingStatus Status { get; set; }
        public decimal PrisMedRabat { get; set; }
        public string? AnvendtRabatType { get; set; }
    }
}
