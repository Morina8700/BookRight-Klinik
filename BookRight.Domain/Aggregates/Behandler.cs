using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Aggregates
{
    public class Behandler
    {
        // Properties
        public Guid BehandlerId { get; private set; }
        public string? Fornavn { get; private set; }
        public string? Efternavn { get; private set; }
        public Email Email { get; private set; }
        public Telefon Telefon { get; private set; }
        public string? AutorisationsNummer { get; private set; }
        public AutorisationsType KrævetAutorisationsType { get; private set; }

        private readonly List<Klinik> _klinikker = new();
        private readonly List<Behandlingstype> _behandlingstyper = new();

        public IReadOnlyCollection<Klinik> Klinikker => _klinikker.AsReadOnly();
        public IReadOnlyCollection<Behandlingstype> Behandlingstyper => _behandlingstyper.AsReadOnly();

        // Constructors
        private Behandler() { } // EF CORE
        public Behandler(string fornavn, string efternavn, string email, string telefon, string autorisationsNummer, AutorisationsType krævetAutorisationsType)
        {
            // Forretningsregler (Starter med at validere input før vi gemmer noget)
            if (string.IsNullOrWhiteSpace(fornavn))
                throw new ArgumentException("Fornavn må ikke være tomt");
            if (string.IsNullOrWhiteSpace(efternavn))
                throw new ArgumentException("Efternavn må ikke være tomt");
            if (string.IsNullOrWhiteSpace(autorisationsNummer))
                throw new ArgumentException("AutorisationsNummer må ikke være tomt");

            BehandlerId = Guid.NewGuid();
            Fornavn = fornavn;
            Efternavn = efternavn;
            Email = new Email(email);
            Telefon = new Telefon(telefon);
            AutorisationsNummer = autorisationsNummer;
            KrævetAutorisationsType = krævetAutorisationsType;
        }

        // Tjekker om behandleren er autoriseret til behandlingstypen
        public bool KanUdføre(Behandlingstype behandlingstype)
            => _behandlingstyper.Any(b => b.BehandlingstypeId == behandlingstype.BehandlingstypeId);

        // Tjekker om behandleren er ansat på klinikken
        public bool ArbejderPå(Guid klinikId)
            => _klinikker.Any(k => k.KlinikId == klinikId);

        // Tilknytter behandleren til en klinik - bruges ved oprettelse af behandler
        // _klinikker er private, så denne metode er den eneste måde at tilføje på (encapsulation)
        public void TilknytKlinik(Klinik klinik)
            => _klinikker.Add(klinik);

        public void TilknytBehandlingstype(Behandlingstype behandlingstype)
        {
            if (behandlingstype.KrævetAutorisationsType != KrævetAutorisationsType)
                throw new InvalidOperationException("Behandleren har ikke den nødvendige autorisation til behandlingstypen");

            _behandlingstyper.Add(behandlingstype);
        }

    }
}
