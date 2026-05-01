using BookRight.Domain.Aggregates;
using BookRight.Domain.Interfaces;
using BookRight.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Repositories
{
    public class BehandlerRepository : IBehandlerRepository
    {
        private readonly BookRightDbContext _context;

        public BehandlerRepository(BookRightDbContext context)
        {
            _context = context;
        }

        // Include henter de relaterede lister med – ellers er Klinikker og Behandlingstyper tomme
        public async Task<Behandler> HentMedDetaljerAsync(Guid behandlerId)
        {
            return await _context.Behandlere
                .Include(b => b.Klinikker)
                .Include(b => b.Behandlingstyper)
                .FirstOrDefaultAsync(b => b.BehandlerId == behandlerId);
        }

        public async Task<IEnumerable<Behandler>> HentAlleAsync()
        {
            return await _context.Behandlere
                .Include(b => b.Klinikker)
                .Include(b => b.Behandlingstyper)
                .ToListAsync();
        }
    }
}