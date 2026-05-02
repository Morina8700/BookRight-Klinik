namespace BookRight.Facade.Contracts.Bookinger
{
    public class KlinikDto
    {
        public Guid KlinikId { get; set; }
        public string Navn { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
    }
}