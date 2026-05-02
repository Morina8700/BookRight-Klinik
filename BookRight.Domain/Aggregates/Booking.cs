using BookRight.Domain.Enums;

namespace BookRight.Domain.Aggregates
{
    public class Booking
    {
        public Guid BookingId { get; private set; }
        public Guid KundeId { get; private set; }
        public Guid BehandlerId { get; private set; }
        public Guid KlinikId { get; private set; }
        public Guid BehandlingstypeId { get; private set; }
        public Guid? KampagneId { get; private set; }

        public DateTime StartTid { get; private set; }
        public DateTime SlutTid { get; private set; }

        public BookingStatus Status { get; private set; }
        public DateTime OprettetDen { get; private set; }

        public decimal PrisUdenRabat { get; private set; }
        public decimal PrisMedRabat { get; private set; }
        public string? AnvendtRabatType { get; private set; }

        private Booking() { }

        public Booking(Guid kundeId, Guid behandlerId, Guid klinikId, Guid behandlingstypeId, DateTime startTid, DateTime slutTid, decimal prisUdenRabat, decimal prisMedRabat, string? anvendtRabatType, Guid? kampagneId = null)
        {
            BookingId = Guid.NewGuid();
            KundeId = kundeId;
            BehandlerId = behandlerId;
            KlinikId = klinikId;
            BehandlingstypeId = behandlingstypeId;
            StartTid = startTid;
            SlutTid = slutTid;
            PrisUdenRabat = prisUdenRabat;
            PrisMedRabat = prisMedRabat;
            AnvendtRabatType = anvendtRabatType;
            KampagneId = kampagneId;
            Status = BookingStatus.Aktiv;
            OprettetDen = DateTime.Now;
        }
    }
}