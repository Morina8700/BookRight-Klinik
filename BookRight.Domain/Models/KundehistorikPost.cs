using BookRight.Domain.Enums;

namespace BookRight.Domain.Models
{
    public class KundehistorikPost
    {
        public Guid BookingId { get; set; }
        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }
        public string KlinikNavn { get; set; } = string.Empty;
        public string BehandlerNavn { get; set; } = string.Empty;
        public string BehandlingstypeNavn { get; set; } = string.Empty;
        public BookingStatus Status { get; set; }
    }
}
