using BookRight.Domain.Enums;

namespace BookRight.Domain.Aggregates
{
    public class Behandler
    {
        public Guid BehandlerId { get; private set; }
        public string Fornavn { get; private set; }
        public string Efternavn { get; private set; }
        public string Email { get; private set; }
        public string Telefon { get; private set; }
        public string AutorisationsNummer { get; private set; }
        public AutorisationsType AutorisationsType { get; private set; }

        private readonly List<Klinik> _klinikker = new();
        private readonly List<Behandlingstype> _behandlingstyper = new();

        public IReadOnlyCollection<Klinik> Klinikker => _klinikker.AsReadOnly();
        public IReadOnlyCollection<Behandlingstype> Behandlingstyper => _behandlingstyper.AsReadOnly();

        private Behandler() { }

        public Behandler(string fornavn, string efternavn, string email, string telefon, string autorisationsNummer, AutorisationsType autorisationsType)
        {
            BehandlerId = Guid.NewGuid();
            Fornavn = fornavn;
            Efternavn = efternavn;
            Email = email;
            Telefon = telefon;
            AutorisationsNummer = autorisationsNummer;
            AutorisationsType = autorisationsType;
        }

        public bool KanUdføre(Behandlingstype behandlingstype)
            => _behandlingstyper.Any(b => b.BehandlingstypeId == behandlingstype.BehandlingstypeId);

        public bool ArbejderPå(Guid klinikId)
            => _klinikker.Any(k => k.KlinikId == klinikId);
    }
}