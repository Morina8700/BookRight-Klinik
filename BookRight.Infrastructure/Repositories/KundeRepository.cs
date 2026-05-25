using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BookRight.Domain.Aggregates;
using BookRight.Domain.Interfaces;
using BookRight.Domain.ValueObjects;
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

        public async Task OpdaterAsync(Kunde kunde)
        {
            _context.Kunder.Update(kunde);
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

        public async Task<bool> EmailFindesAsync(string email)
        {
            var emailValue = new Email(email);

            return await _context.Kunder
                .AnyAsync(k => k.Email == emailValue);
        }

        public async Task<bool> TelefonFindesAsync(string telefon)
        {
            var telefonValue = new Telefon(telefon);

            return await _context.Kunder
                .AnyAsync(k => k.Telefon == telefonValue);
        }
    }
}
