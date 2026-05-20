namespace BookRight.Facade.Contracts.Bookinger
{
    public class BehandlerDto
    {
        public Guid BehandlerId { get; set; }
        public string? Fornavn { get; set; }
        public string? Efternavn { get; set; }

        // Bruges i UI til at vise behandleren ved de klinikker, hvor behandleren arbejder
        public List<Guid> KlinikIds { get; set; } = new();

        // Bruges i UI til at vise de behandlingstyper, som behandleren må udføre
        public List<Guid> BehandlingstypeIds { get; set; } = new();
    }
}