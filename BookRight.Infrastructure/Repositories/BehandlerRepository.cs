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

        // Henter en behandler med de klinikker og behandlingstyper, der skal bruges til validering
        public async Task<Behandler?> HentEfterIdAsync(Guid behandlerId) 
        {
            return await _context.Behandlere
                .Include(b => b.Klinikker)
                .Include(b => b.Behandlingstyper)
                .FirstOrDefaultAsync(b => b.BehandlerId == behandlerId);
        }

        // Henter alle behandlere med deres klinikker og behandlingstyper
        public async Task<IEnumerable<Behandler>> HentAlleAsync()
        {
            return await _context.Behandlere
                .Include(b => b.Klinikker)
                .Include(b => b.Behandlingstyper)
                .ToListAsync();
        }
        public async Task AddAsync(Behandler behandler)
        {
            await _context.Behandlere.AddAsync(behandler);
            await _context.SaveChangesAsync();
        }
    }
}