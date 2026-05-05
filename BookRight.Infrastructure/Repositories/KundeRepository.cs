using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BookRight.Domain.Aggregates;
using BookRight.Domain.Interfaces;
using BookRight.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Repositories
{
    public class KundeRepository : IKundeRepository
    {
        private readonly BookRightDbContext _context;

        public KundeRepository(BookRightDbContext context)
        {
            _context = context;
        }

        public async Task TilføjAsync(Kunde kunde)
        {
            await _context.Kunder.AddAsync(kunde);
            await _context.SaveChangesAsync();
        }

        public async Task<Kunde?> HentPåIdAsync(Guid kundeId)
        {
            return await _context.Kunder
                .FirstOrDefaultAsync(k => k.KundeId == kundeId);
        }

        public async Task<IEnumerable<Kunde>> HentAlleAsync()
        {
            return await _context.Kunder
                .OrderBy(k => k.Fornavn)
                .ThenBy(k => k.Efternavn)
                .ToListAsync();
        }
    }
}
