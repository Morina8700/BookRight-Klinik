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

        public async Task<Behandlingstype> HentAsync(Guid behandlingstypeId)
        {
            return await _context.Behandlingstyper
                .FirstOrDefaultAsync(bt => bt.BehandlingstypeId == behandlingstypeId);
        }

        public async Task<IEnumerable<Behandlingstype>> HentAlleAsync()
        {
            return await _context.Behandlingstyper.ToListAsync();
        }
    }
}