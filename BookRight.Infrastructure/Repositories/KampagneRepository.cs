using BookRight.Domain.Aggregates;
using BookRight.Domain.Interfaces;
using BookRight.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Repositories
{
    public class KampagneRepository : IKampagneRepository
    {
        private readonly BookRightDbContext _context;

        public KampagneRepository(BookRightDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Kampagne>> HentAktiveKampagnerAsync(DateOnly dato) 
        {
            return await _context.Kampagner
                .Where(k => k.Aktiv) //Henter kun aktive kampagner
                .ToListAsync(); //Henter alle kampagner fra databasen og returnerer dem som en liste
        }
    }
}
