using BookRight.Facade.Contracts.Behandler;

namespace BookRight.Facade.Contracts.Bookinger
{
    public class BehandlingstypeDto
    {
        public Guid BehandlingstypeId { get; set; }
        public string Navn { get; set; }
        public decimal Pris { get; set; }
        public int VarighedMinutter { get; set; }
        public AutorisationsTypeDTO KrævetAutorisationsType { get; set; }

    }
}