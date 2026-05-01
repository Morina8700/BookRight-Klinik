namespace BookRight.Facade.Contracts.Bookinger
{
    public class OpretBookingRequest
    {
        public Guid KundeId { get; set; }
        public Guid BehandlerId { get; set; }
        public Guid KlinikId { get; set; }
        public Guid BehandlingstypeId { get; set; }
        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }
        public decimal PrisUdenRabat { get; set; }
        public decimal PrisMedRabat { get; set; }
        public string? AnvendtRabatType { get; set; }
        public Guid? KampagneId { get; set; }
    }
}