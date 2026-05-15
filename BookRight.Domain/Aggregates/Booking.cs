using BookRight.Domain.Enums;

namespace BookRight.Domain.Aggregates
{
    public class Booking
    {
        // Properties
        public Guid BookingId { get; private set; }
        public Guid KundeId { get; private set; }
        public Guid BehandlerId { get; private set; }
        public Guid KlinikId { get; private set; }
        public Guid BehandlingstypeId { get; private set; }
        public DateTime StartTid { get; private set; }
        public DateTime SlutTid { get; private set; }
        public BookingStatus Status { get; private set; }
        public DateTime OprettelsesDato { get; private set; }
        public decimal PrisUdenRabat { get; private set; }
        public decimal PrisMedRabat { get; private set; }
        public string? AnvendtRabatType { get; private set; }

        // Constructors
        private Booking() { } // EF CORE
        public Booking(Guid kundeId, Guid behandlerId, Guid klinikId, Guid behandlingstypeId, DateTime startTid, DateTime slutTid, decimal prisUdenRabat, decimal prisMedRabat, string? anvendtRabatType)
        {
        // Forretningsregler (Starter med at valider input før vi gemmer noget)
            if (kundeId == Guid.Empty) throw new ArgumentException("KundeId må ikke være tomt");
            if (behandlerId == Guid.Empty) throw new ArgumentException("BehandlerId må ikke være tomt");
            if (klinikId == Guid.Empty) throw new ArgumentException("KlinikId må ikke være tomt");
            if (behandlingstypeId == Guid.Empty) throw new ArgumentException("BehandlingstypeId må ikke være tomt");

            if (startTid >= slutTid) throw new ArgumentException("StartTid skal være før SlutTid");
            if (startTid < DateTime.Now) throw new ArgumentException("StartTid må ikke være i fortiden");

            if (prisUdenRabat < 0) throw new ArgumentException("PrisUdenRabat må ikke være negativ");
            if (prisMedRabat < 0) throw new ArgumentException("PrisMedRabat må ikke være negativ");

            BookingId = Guid.NewGuid();
            KundeId = kundeId;
            BehandlerId = behandlerId;
            KlinikId = klinikId;
            BehandlingstypeId = behandlingstypeId;
            StartTid = startTid;
            SlutTid = slutTid;
            PrisUdenRabat = prisUdenRabat;
            PrisMedRabat = prisMedRabat;
            AnvendtRabatType = anvendtRabatType;
            Status = BookingStatus.Aktiv;
            OprettelsesDato = DateTime.Now;
        }

        // Statusmetoder (forretningsregler)
        public void Aflys()
        {
            // En booking der allerede er afsluttet, må ikke aflyses bagefter.
            if (Status == BookingStatus.Afsluttet)
                throw new InvalidOperationException("En afsluttet booking kan ikke aflyses");
            Status = BookingStatus.Aflyst;
        }

        public void MarkerAnkommet()
        {
            // Kun aktive bookinger kan markeres som ankommet.
            if (Status != BookingStatus.Aktiv)
                throw new InvalidOperationException("Kun en aktiv booking kan markeres som ankommet");
            Status = BookingStatus.Ankommet;
        }

        public void MarkerAfsluttet()
        {
            // En booking skal først være markeret som ankommet, før den kan afsluttes.
            if (Status != BookingStatus.Ankommet)
                throw new InvalidOperationException("Booking skal være ankommet før den kan afsluttes");
            Status = BookingStatus.Afsluttet;
        }

        public void MarkerNoShow()
        {
            // No-show betyder, at kunden ikke mødte op til en aktiv booking.
            if (Status != BookingStatus.Aktiv)
                throw new InvalidOperationException("Kun en aktiv booking kan markeres som NoShow");
            Status = BookingStatus.NoShow;
        }
    }
}