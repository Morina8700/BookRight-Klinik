namespace BookRight.Facade.Contracts.Bookinger
{
    public class KlinikDto
    {
        public Guid KlinikId { get; set; }
        public string Navn { get; set; }
        public string Adresse { get; set; }
        public int AntalRum { get; set; }
    }
}