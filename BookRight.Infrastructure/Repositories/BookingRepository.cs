using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.Interfaces;
using BookRight.UseCases.Queries.Kundehistorik;
using BookRight.UseCases.Queries.Kalender;
using BookRight.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Repositories
{
    // INFRASTRUCTURE  (Implementerer IBookingRepository med EF Core)
    public class BookingRepository : IBookingRepository, IBookingStatusRepository, IBookingQueryRepository, IKundehistorikQueryRepository
    {
        private readonly BookRightDbContext _context;

        public BookingRepository(BookRightDbContext context)
        {
            _context = context;
        }

        // Tjekker om behandleren allerede har en aktiv booking i det ønskede tidsrum
        // En booking overlapper hvis den starter før vores slut OG slutter efter vores start
        public async Task<bool> ErBehandlerLedigAsync(Guid behandlerId, DateTime startTid, DateTime slutTid)
        {
            return !await _context.Bookinger.AnyAsync(b =>
                b.BehandlerId == behandlerId &&
                b.Status == BookingStatus.Aktiv &&
                b.StartTid < slutTid &&
                b.SlutTid > startTid);
        }

        // Tæller bookinger der overlapper i tidsrummet på klinikken
        public async Task<int> HentAntalOverlappendeBookingerAsync(Guid klinikId, DateTime startTid, DateTime slutTid)
        {
            return await _context.Bookinger.CountAsync(b =>
                b.KlinikId == klinikId &&
                b.Status == BookingStatus.Aktiv &&
                b.StartTid < slutTid &&
                b.SlutTid > startTid);
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Bookinger.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking?> HentPåIdAsync(Guid bookingId)
        {
            return await _context.Bookinger
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task OpdaterAsync(Booking booking)
        {
            _context.Bookinger.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BookingKalenderPost>> HentBookingerForDatoAsync(DateOnly dato)
        {
            var start = dato.ToDateTime(TimeOnly.MinValue);
            var slut = dato.ToDateTime(TimeOnly.MaxValue);

            return await (
                from booking in _context.Bookinger
                join kunde in _context.Kunder
                    on booking.KundeId equals kunde.KundeId
                join behandler in _context.Behandlere
                    on booking.BehandlerId equals behandler.BehandlerId
                join behandlingstype in _context.Behandlingstyper
                    on booking.BehandlingstypeId equals behandlingstype.BehandlingstypeId
                where booking.StartTid >= start && booking.StartTid <= slut
                orderby booking.StartTid
                select new BookingKalenderPost(
                    booking.BookingId,
                    kunde.Fornavn + " " + kunde.Efternavn,
                    behandler.Fornavn + " " + behandler.Efternavn,
                    behandlingstype.Navn ?? "Ukendt behandling",
                    booking.StartTid,
                    booking.SlutTid,
                    booking.Status.ToString(),
                    booking.PrisUdenRabat,
                    booking.PrisMedRabat,
                    booking.AnvendtRabatType ?? "Ingen"
                )
            ).ToListAsync();
        }

        // Henter kundens tidligere bookinger, som er relevante for kundehistorik
        public async Task<IEnumerable<KundehistorikPost>> HentForKundeAsync(Guid kundeId)
        {
            return await (
                from booking in _context.Bookinger
                join behandler in _context.Behandlere
                    on booking.BehandlerId equals behandler.BehandlerId
                join behandlingstype in _context.Behandlingstyper
                    on booking.BehandlingstypeId equals behandlingstype.BehandlingstypeId
                join klinik in _context.Klinikker
                    on booking.KlinikId equals klinik.KlinikId
                where booking.KundeId == kundeId &&
                      (booking.Status == BookingStatus.Afsluttet ||
                       booking.Status == BookingStatus.Aflyst ||
                       booking.Status == BookingStatus.NoShow)
                orderby booking.StartTid descending
                select new KundehistorikPost(
                    booking.BookingId,
                    booking.StartTid,
                    booking.SlutTid,
                    behandlingstype.Navn ?? string.Empty,
                    (behandler.Fornavn ?? "") + " " + (behandler.Efternavn ?? ""),
                    klinik.Navn ?? string.Empty,
                    booking.Status,
                    booking.PrisMedRabat,
                    booking.AnvendtRabatType
                )
            ).ToListAsync();
        }
    }
}