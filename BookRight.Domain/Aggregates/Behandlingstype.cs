using BookRight.Domain.Enums;

namespace BookRight.Domain.Aggregates
{
    public class Behandlingstype
    {
        public Guid BehandlingstypeId { get; private set; }
        public string Navn { get; private set; }
        public int VarighedMinutter { get; private set; }
        public decimal Pris { get; private set; }
        public AutorisationsType KrævetAutorisationsType { get; private set; }

        private Behandlingstype() { }

        public Behandlingstype(string navn, int varighedMinutter, decimal pris, AutorisationsType krævetType)
        {
            BehandlingstypeId = Guid.NewGuid();
            Navn = navn;
            VarighedMinutter = varighedMinutter;
            Pris = pris;
            KrævetAutorisationsType = krævetType;
        }
    }
}