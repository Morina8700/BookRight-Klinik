using BookRight.Domain.Aggregates;
using BookRight.Domain.Interfaces;
using BookRight.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Repositories
{
    public class BehandlingstypeRepository : IBehandlingstypeRepository
    {
        private readonly BookRightDbContext _context;
        public BehandlingstypeRepository(BookRightDbContext context) 
        {
            _context = context;
        }

        // Henter en behandlingstype ud fra id, eller null hvis den ikke findes
        public async Task<Behandlingstype?> HentEfterIdAsync(Guid behandlingstypeId) 
        {
            return await _context.Behandlingstyper
                .FirstOrDefaultAsync(b => b.BehandlingstypeId == behandlingstypeId);
        }

        // Henter alle behandlingstyper fra databasen og returnerer dem som en liste
        public async Task<IEnumerable<Behandlingstype>> HentAlleAsync()
        {
            return await _context.Behandlingstyper.ToListAsync();
        }
    }
}