namespace BookRight.Domain.Aggregates
{
    public class Klinik
    {
        public Guid KlinikId { get; private set; }
        public string Navn { get; private set; }
        public string Adresse { get; private set; }
        public int AntalRum { get; private set; }

        private Klinik() { }

        public Klinik(string navn, string adresse, int antalRum)
        {
            KlinikId = Guid.NewGuid();
            Navn = navn;
            Adresse = adresse;
            AntalRum = antalRum;
        }
    }
}