namespace BookRight.Facade.Contracts.Bookinger
{
    // Indeholder de oplysninger receptionisten sender ind for at oprette en booking
    public class OpretBookingRequest
    {
        public Guid KundeId { get; set; }
        public Guid BehandlerId { get; set; }
        public Guid KlinikId { get; set; }
        public Guid BehandlingstypeId { get; set; }
        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }
    }
}