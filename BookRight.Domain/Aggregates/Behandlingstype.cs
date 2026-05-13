using BookRight.Domain.Enums;

namespace BookRight.Domain.Aggregates
{
    public class Behandlingstype
    {
        // Properties
        public Guid BehandlingstypeId { get; private set; }
        public string? Navn { get; private set; }
        public int VarighedMinutter { get; private set; }
        public decimal Pris { get; private set; }
        public AutorisationsType KrævetAutorisationsType { get; private set; }
        public BehandlingsType Type { get; private set; }

        // Constructors
        private Behandlingstype() { } // EF CORE
        public Behandlingstype(string navn, int varighedMinutter, decimal pris, AutorisationsType krævetAutorisationsType, BehandlingsType type)
        {
            // Forretningsregler (Starter med at validere input før vi gemmer noget)
            if (string.IsNullOrWhiteSpace(navn))
                throw new ArgumentException("Navn må ikke være tomt");
            if (varighedMinutter <= 0)
                throw new ArgumentException("Varighed skal være positiv");
            if (pris < 0)
                throw new ArgumentException("Pris må ikke være negativ");
            BehandlingstypeId = Guid.NewGuid();
            Navn = navn;
            VarighedMinutter = varighedMinutter;
            Pris = pris;
            KrævetAutorisationsType = krævetAutorisationsType;
            Type = type;
        }
    }
}