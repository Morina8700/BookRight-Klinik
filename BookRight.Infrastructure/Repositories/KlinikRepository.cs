using BookRight.Domain.Aggregates;
using BookRight.Domain.Interfaces;
using BookRight.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Repositories
{
    public class KlinikRepository : IKlinikRepository
    {
        // Giver adgang til databasen via Entity Framework
        private readonly BookRightDbContext _context;
        public KlinikRepository(BookRightDbContext context) 
        {
            _context = context;
        }

        // Henter en klinik ud fra id, eller null hvis den ikke findes
        public async Task<Klinik?> HentEfterIdAsync(Guid klinikId) 
        {
            return await _context.Klinikker
                .FirstOrDefaultAsync(k => k.KlinikId == klinikId);
        }

        // Henter alle klinikker fra databasen og returnerer dem som en liste
        public async Task<IEnumerable<Klinik>> HentAlleAsync()
        {
            return await _context.Klinikker.ToListAsync(); 
        }
    }
}