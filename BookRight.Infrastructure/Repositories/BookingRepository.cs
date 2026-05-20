using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.Interfaces;
using BookRight.Domain.Models;
using BookRight.Infrastructure.Persistence;
using BookRight.UseCases.Queries.Kalender;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Repositories
{
        // INFRASTRUCTURE  (Implementerer IBookingRepository med EF Core)
    public class BookingRepository : IBookingRepository, IBookingStatusRepository, IBookingQueryRepository
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
        public async Task<IEnumerable<KundehistorikPost>> HentKundehistorikAsync(Guid kundeId)
        {
            return await _context.Bookinger
                .Where(b => b.KundeId == kundeId &&
                    (b.Status == BookingStatus.Afsluttet ||
                     b.Status == BookingStatus.Aflyst ||
                     b.Status == BookingStatus.NoShow))
                .OrderByDescending(b => b.StartTid)
                .Select(b => new KundehistorikPost
                {
                    BookingId = b.BookingId,
                    StartTid = b.StartTid,
                    SlutTid = b.SlutTid,
                    Status = b.Status,
                    PrisMedRabat = b.PrisMedRabat,
                    AnvendtRabatType = b.AnvendtRabatType
                })
                .ToListAsync();
        }
    }
}