using BookRight.Domain.Aggregates;
using BookRight.Domain.Interfaces;
using BookRight.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Repositories
{
    public class KlinikRepository : IKlinikRepository
    {
        private readonly BookRightDbContext _context;

        public KlinikRepository(BookRightDbContext context)
        {
            _context = context;
        }

        public async Task<Klinik> HentAsync(Guid klinikId)
        {
            return await _context.Klinikker
                .FirstOrDefaultAsync(k => k.KlinikId == klinikId);
        }

        public async Task<IEnumerable<Klinik>> HentAlleAsync()
        {
            return await _context.Klinikker.ToListAsync();
        }
    }
}