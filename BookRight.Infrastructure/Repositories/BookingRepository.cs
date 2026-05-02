using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.Interfaces;
using BookRight.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookRightDbContext _context;

        public BookingRepository(BookRightDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Bookinger.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        // Tjekker om behandleren allerede har en aktiv booking i det ønskede tidsrum
        // En booking overlapper hvis den starter før vores slut OG slutter efter vores start
        public async Task<bool> ErBehandlerLedigAsync(Guid behandlerId, DateTime startTid, DateTime slutTid)
        {
            var harKonflikt = await _context.Bookinger
                .AnyAsync(b =>
                    b.BehandlerId == behandlerId &&
                    b.Status == BookingStatus.Aktiv &&
                    b.StartTid < slutTid &&
                    b.SlutTid > startTid);

            return !harKonflikt;
        }

        // Tæller hvor mange aktive bookinger der er i klinikken i tidsrummet
        // Bruges til at tjekke om alle rum er optaget
        public async Task<int> HentAntalAktiveBookingerAsync(Guid klinikId, DateTime startTid, DateTime slutTid)
        {
            return await _context.Bookinger
                .CountAsync(b =>
                    b.KlinikId == klinikId &&
                    b.Status == BookingStatus.Aktiv &&
                    b.StartTid < slutTid &&
                    b.SlutTid > startTid);
        }
    }
}