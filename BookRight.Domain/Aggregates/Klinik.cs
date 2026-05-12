namespace BookRight.Domain.Aggregates
{
    public class Klinik
    {
        // Properties
        public Guid KlinikId { get; private set; }
        public string? Navn { get; private set; }
        public string? Adresse { get; private set; }
        public int AntalRum { get; private set; }

        private readonly List<Behandler> _behandlere = new();
        public IReadOnlyCollection<Behandler> Behandlere => _behandlere.AsReadOnly();


        // Constructors
        private Klinik() { } // EF CORE
        public Klinik(string navn, string adresse, int antalRum)
        {
        // Forretningsregler (Starter med at validere input før vi gemmer noget)
            if (string.IsNullOrWhiteSpace(navn))
                throw new ArgumentException("Navn må ikke være tomt");
            if (string.IsNullOrWhiteSpace(adresse))
                throw new ArgumentException("Adresse må ikke være tomt");
            if (antalRum <= 0)
                throw new ArgumentException("AntalRum skal være større end 0");

            KlinikId = Guid.NewGuid();
            Navn = navn;
            Adresse = adresse;
            AntalRum = antalRum;
        }

        // Tjekker om klinikken har ledige rum på et givet tidspunkt
        public bool HarLedigeRum(int aktiveBookinger)
            => aktiveBookinger < AntalRum;
    }
}