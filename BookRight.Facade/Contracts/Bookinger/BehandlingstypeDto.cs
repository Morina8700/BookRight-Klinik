namespace BookRight.Facade.Contracts.Bookinger
{
    public class BehandlingstypeDto
    {
        public Guid BehandlingstypeId { get; set; }
        public string Navn { get; set; } = string.Empty;
        public decimal Pris { get; set; }
        public int VarighedMinutter { get; set; }
    }
}