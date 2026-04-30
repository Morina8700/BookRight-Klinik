using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BookRight.Domain.Aggregates;

namespace BookRight.Infrastructure.Persistence
{
    public class BookRightDbContext : DbContext
    {
        public BookRightDbContext(DbContextOptions<BookRightDbContext> options) : base(options)
        {
        }
       
        public DbSet<Kunde> Kunder => Set<Kunde>();
    }
}
