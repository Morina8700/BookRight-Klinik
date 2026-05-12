using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BookRight.Infrastructure.Persistence
{
    public class BookRightDbContext : DbContext
    {
        public BookRightDbContext(DbContextOptions<BookRightDbContext> options) : base(options) { }

        public DbSet<Kunde> Kunder => Set<Kunde>();
        public DbSet<Behandler> Behandlere => Set<Behandler>();
        public DbSet<Klinik> Klinikker => Set<Klinik>();
        public DbSet<Behandlingstype> Behandlingstyper => Set<Behandlingstype>();
        public DbSet<Booking> Bookinger => Set<Booking>();

        public DbSet<Kampagne> Kampagner => Set<Kampagne>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Decimal precision
            modelBuilder.Entity<Behandlingstype>()
                .Property(b => b.Pris)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Booking>()
                .Property(b => b.PrisMedRabat)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Booking>()
                .Property(b => b.PrisUdenRabat)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Behandler>()
                .Property(b => b.KrævetAutorisationsType)
                .HasColumnName("AutorisationsType");

            var klinik1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var klinik2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var klinik3Id = Guid.Parse("33333333-3333-3333-3333-333333333333");

            var fysio30Id = Guid.Parse("aaaaaaaa-0001-0000-0000-000000000000");
            var fysio45Id = Guid.Parse("aaaaaaaa-0002-0000-0000-000000000000");
            var fysio60Id = Guid.Parse("aaaaaaaa-0003-0000-0000-000000000000");
            var massage30Id = Guid.Parse("aaaaaaaa-0004-0000-0000-000000000000");
            var massage60Id = Guid.Parse("aaaaaaaa-0005-0000-0000-000000000000");
            var akupunkturId = Guid.Parse("aaaaaaaa-0006-0000-0000-000000000000");
            var kostFørsteId = Guid.Parse("aaaaaaaa-0007-0000-0000-000000000000");
            var kostOpfølgId = Guid.Parse("aaaaaaaa-0008-0000-0000-000000000000");
            var holdId = Guid.Parse("aaaaaaaa-0009-0000-0000-000000000000");

            var b1 = Guid.Parse("bbbbbbbb-0001-0000-0000-000000000000");
            var b2 = Guid.Parse("bbbbbbbb-0002-0000-0000-000000000000");
            var b3 = Guid.Parse("bbbbbbbb-0003-0000-0000-000000000000");
            var b4 = Guid.Parse("bbbbbbbb-0004-0000-0000-000000000000");
            var b5 = Guid.Parse("bbbbbbbb-0005-0000-0000-000000000000");
            var b6 = Guid.Parse("bbbbbbbb-0006-0000-0000-000000000000");
            var b7 = Guid.Parse("bbbbbbbb-0007-0000-0000-000000000000");
            var b8 = Guid.Parse("bbbbbbbb-0008-0000-0000-000000000000");
            var b9 = Guid.Parse("bbbbbbbb-0009-0000-0000-000000000000");
            var b10 = Guid.Parse("bbbbbbbb-0010-0000-0000-000000000000");
            var b11 = Guid.Parse("bbbbbbbb-0011-0000-0000-000000000000");
            var b12 = Guid.Parse("bbbbbbbb-0012-0000-0000-000000000000");

            // Klinikker
            modelBuilder.Entity<Klinik>().HasData(
                new { KlinikId = klinik1Id, Navn = "BookRight Vejle", Adresse = "Vejle Centervej 1, 7100 Vejle", AntalRum = 4 },
                new { KlinikId = klinik2Id, Navn = "BookRight Egtved", Adresse = "Egtvedvej 5, 6040 Egtved", AntalRum = 3 },
                new { KlinikId = klinik3Id, Navn = "BookRight Kolding", Adresse = "Kolding Storcenter 2, 6000 Kolding", AntalRum = 3 }
            );

            // Behandlingstyper
            modelBuilder.Entity<Behandlingstype>().HasData(
                new { BehandlingstypeId = fysio30Id, Navn = "Fysioterapi 30 min", VarighedMinutter = 30, Pris = 395m, KrævetAutorisationsType = AutorisationsType.Fysioterapeut },
                new { BehandlingstypeId = fysio45Id, Navn = "Fysioterapi 45 min", VarighedMinutter = 45, Pris = 589m, KrævetAutorisationsType = AutorisationsType.Fysioterapeut },
                new { BehandlingstypeId = fysio60Id, Navn = "Fysioterapi 60 min", VarighedMinutter = 60, Pris = 745m, KrævetAutorisationsType = AutorisationsType.Fysioterapeut },
                new { BehandlingstypeId = massage30Id, Navn = "Sportsmassage 30 min", VarighedMinutter = 30, Pris = 350m, KrævetAutorisationsType = AutorisationsType.Massør },
                new { BehandlingstypeId = massage60Id, Navn = "Sportsmassage 60 min", VarighedMinutter = 60, Pris = 699m, KrævetAutorisationsType = AutorisationsType.Massør },
                new { BehandlingstypeId = akupunkturId, Navn = "Akupunktur 45 min", VarighedMinutter = 45, Pris = 550m, KrævetAutorisationsType = AutorisationsType.Akupunktør },
                new { BehandlingstypeId = kostFørsteId, Navn = "Kostvejledning førstegangskons.", VarighedMinutter = 60, Pris = 799m, KrævetAutorisationsType = AutorisationsType.Kostvejleder },
                new { BehandlingstypeId = kostOpfølgId, Navn = "Kostvejledning opfølgning", VarighedMinutter = 30, Pris = 450m, KrævetAutorisationsType = AutorisationsType.Kostvejleder },
                new { BehandlingstypeId = holdId, Navn = "Holdtræning/genoptræning", VarighedMinutter = 60, Pris = 150m, KrævetAutorisationsType = AutorisationsType.Fysioterapeut }
            );

            // Behandlere
            modelBuilder.Entity<Behandler>().HasData(
                new { BehandlerId = b1, Fornavn = "Anders", Efternavn = "Nielsen", Email = "anders@bookright.dk", Telefon = "11111101", AutorisationsNummer = "FYS-001", KrævetAutorisationsType = AutorisationsType.Fysioterapeut },
                new { BehandlerId = b2, Fornavn = "Birgitte", Efternavn = "Hansen", Email = "birgitte@bookright.dk", Telefon = "11111102", AutorisationsNummer = "FYS-002", KrævetAutorisationsType = AutorisationsType.Fysioterapeut },
                new { BehandlerId = b3, Fornavn = "Casper", Efternavn = "Madsen", Email = "casper@bookright.dk", Telefon = "11111103", AutorisationsNummer = "FYS-003", KrævetAutorisationsType = AutorisationsType.Fysioterapeut },
                new { BehandlerId = b4, Fornavn = "Diana", Efternavn = "Sørensen", Email = "diana@bookright.dk", Telefon = "11111104", AutorisationsNummer = "FYS-004", KrævetAutorisationsType = AutorisationsType.Fysioterapeut },
                new { BehandlerId = b5, Fornavn = "Erik", Efternavn = "Christensen", Email = "erik@bookright.dk", Telefon = "11111105", AutorisationsNummer = "MAS-001", KrævetAutorisationsType = AutorisationsType.Massør },
                new { BehandlerId = b6, Fornavn = "Freja", Efternavn = "Pedersen", Email = "freja@bookright.dk", Telefon = "11111106", AutorisationsNummer = "MAS-002", KrævetAutorisationsType = AutorisationsType.Massør },
                new { BehandlerId = b7, Fornavn = "Gunnar", Efternavn = "Jensen", Email = "gunnar@bookright.dk", Telefon = "11111107", AutorisationsNummer = "MAS-003", KrævetAutorisationsType = AutorisationsType.Massør },
                new { BehandlerId = b8, Fornavn = "Hanne", Efternavn = "Larsen", Email = "hanne@bookright.dk", Telefon = "11111108", AutorisationsNummer = "AKU-001", KrævetAutorisationsType = AutorisationsType.Akupunktør },
                new { BehandlerId = b9, Fornavn = "Ivan", Efternavn = "Olsen", Email = "ivan@bookright.dk", Telefon = "11111109", AutorisationsNummer = "AKU-002", KrævetAutorisationsType = AutorisationsType.Akupunktør },
                new { BehandlerId = b10, Fornavn = "Julie", Efternavn = "Thomsen", Email = "julie@bookright.dk", Telefon = "11111110", AutorisationsNummer = "AKU-003", KrævetAutorisationsType = AutorisationsType.Akupunktør },
                new { BehandlerId = b11, Fornavn = "Klaus", Efternavn = "Andersen", Email = "klaus@bookright.dk", Telefon = "11111111", AutorisationsNummer = "KOS-001", KrævetAutorisationsType = AutorisationsType.Kostvejleder },
                new { BehandlerId = b12, Fornavn = "Laura", Efternavn = "Møller", Email = "laura@bookright.dk", Telefon = "11111112", AutorisationsNummer = "KOS-002", KrævetAutorisationsType = AutorisationsType.Kostvejleder }
            );

            // Behandler <-> Klinik
            modelBuilder.Entity<Behandler>()
                .HasMany(b => b.Klinikker)
                .WithMany(k => k.Behandlere)
                .UsingEntity<Dictionary<string, object>>(
                    "BehandlerKlinikker",
                    j => j.HasOne<Klinik>().WithMany().HasForeignKey("KlinikkerKlinikId"),
                    j => j.HasOne<Behandler>().WithMany().HasForeignKey("BehandlerId"),
                    j => j.HasData(
                        new { KlinikkerKlinikId = klinik1Id, BehandlerId = b1 },
                        new { KlinikkerKlinikId = klinik2Id, BehandlerId = b1 },
                        new { KlinikkerKlinikId = klinik1Id, BehandlerId = b2 },
                        new { KlinikkerKlinikId = klinik3Id, BehandlerId = b2 },
                        new { KlinikkerKlinikId = klinik2Id, BehandlerId = b3 },
                        new { KlinikkerKlinikId = klinik3Id, BehandlerId = b3 },
                        new { KlinikkerKlinikId = klinik1Id, BehandlerId = b4 },
                        new { KlinikkerKlinikId = klinik2Id, BehandlerId = b4 },
                        new { KlinikkerKlinikId = klinik1Id, BehandlerId = b5 },
                        new { KlinikkerKlinikId = klinik2Id, BehandlerId = b5 },
                        new { KlinikkerKlinikId = klinik2Id, BehandlerId = b6 },
                        new { KlinikkerKlinikId = klinik3Id, BehandlerId = b6 },
                        new { KlinikkerKlinikId = klinik1Id, BehandlerId = b7 },
                        new { KlinikkerKlinikId = klinik3Id, BehandlerId = b7 },
                        new { KlinikkerKlinikId = klinik1Id, BehandlerId = b8 },
                        new { KlinikkerKlinikId = klinik2Id, BehandlerId = b8 },
                        new { KlinikkerKlinikId = klinik2Id, BehandlerId = b9 },
                        new { KlinikkerKlinikId = klinik3Id, BehandlerId = b9 },
                        new { KlinikkerKlinikId = klinik1Id, BehandlerId = b10 },
                        new { KlinikkerKlinikId = klinik3Id, BehandlerId = b10 },
                        new { KlinikkerKlinikId = klinik1Id, BehandlerId = b11 },
                        new { KlinikkerKlinikId = klinik2Id, BehandlerId = b12 }
                    ));

            // Behandler <-> Behandlingstype
            modelBuilder.Entity<Behandler>()
                .HasMany(b => b.Behandlingstyper)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "BehandlerBehandlingstyper",
                    j => j.HasOne<Behandlingstype>().WithMany().HasForeignKey("BehandlingstyperBehandlingstypeId"),
                    j => j.HasOne<Behandler>().WithMany().HasForeignKey("BehandlerId"),
                    j => j.HasData(
                        new { BehandlingstyperBehandlingstypeId = fysio30Id, BehandlerId = b1 },
                        new { BehandlingstyperBehandlingstypeId = fysio45Id, BehandlerId = b1 },
                        new { BehandlingstyperBehandlingstypeId = fysio60Id, BehandlerId = b1 },
                        new { BehandlingstyperBehandlingstypeId = holdId, BehandlerId = b1 },
                        new { BehandlingstyperBehandlingstypeId = fysio30Id, BehandlerId = b2 },
                        new { BehandlingstyperBehandlingstypeId = fysio45Id, BehandlerId = b2 },
                        new { BehandlingstyperBehandlingstypeId = fysio60Id, BehandlerId = b2 },
                        new { BehandlingstyperBehandlingstypeId = holdId, BehandlerId = b2 },
                        new { BehandlingstyperBehandlingstypeId = fysio30Id, BehandlerId = b3 },
                        new { BehandlingstyperBehandlingstypeId = fysio45Id, BehandlerId = b3 },
                        new { BehandlingstyperBehandlingstypeId = fysio60Id, BehandlerId = b3 },
                        new { BehandlingstyperBehandlingstypeId = holdId, BehandlerId = b3 },
                        new { BehandlingstyperBehandlingstypeId = fysio30Id, BehandlerId = b4 },
                        new { BehandlingstyperBehandlingstypeId = fysio45Id, BehandlerId = b4 },
                        new { BehandlingstyperBehandlingstypeId = fysio60Id, BehandlerId = b4 },
                        new { BehandlingstyperBehandlingstypeId = holdId, BehandlerId = b4 },
                        new { BehandlingstyperBehandlingstypeId = massage30Id, BehandlerId = b5 },
                        new { BehandlingstyperBehandlingstypeId = massage60Id, BehandlerId = b5 },
                        new { BehandlingstyperBehandlingstypeId = massage30Id, BehandlerId = b6 },
                        new { BehandlingstyperBehandlingstypeId = massage60Id, BehandlerId = b6 },
                        new { BehandlingstyperBehandlingstypeId = massage30Id, BehandlerId = b7 },
                        new { BehandlingstyperBehandlingstypeId = massage60Id, BehandlerId = b7 },
                        new { BehandlingstyperBehandlingstypeId = akupunkturId, BehandlerId = b8 },
                        new { BehandlingstyperBehandlingstypeId = akupunkturId, BehandlerId = b9 },
                        new { BehandlingstyperBehandlingstypeId = akupunkturId, BehandlerId = b10 },
                        new { BehandlingstyperBehandlingstypeId = kostFørsteId, BehandlerId = b11 },
                        new { BehandlingstyperBehandlingstypeId = kostOpfølgId, BehandlerId = b11 },
                        new { BehandlingstyperBehandlingstypeId = kostFørsteId, BehandlerId = b12 },
                        new { BehandlingstyperBehandlingstypeId = kostOpfølgId, BehandlerId = b12 }
                    ));

            // 1. Mapping
            modelBuilder.Entity<Kampagne>(entity =>
            {
                entity.HasKey(k => k.KampagneId);

                entity.OwnsOne(k => k.Periode, periode =>
                {
                    periode.Property(p => p.StartDato).HasColumnName("StartDato");
                    periode.Property(p => p.SlutDato).HasColumnName("SlutDato");
                });

                entity.OwnsOne(k => k.Rabatprocent, rabat =>
                {
                    rabat.Property(r => r.Value)
                        .HasColumnName("RabatProcent")
                        .HasPrecision(5, 2);
                });

                entity.Property(k => k.GaeldendeBehandlingstyper)
                    .HasConversion(
                        v => string.Join(",", v),
                        v => v.Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => Enum.Parse<BehandlingsType>(x))
                            .ToList()
                    );
            });

            // 2. Kampagne seed
            var kampagne1Id = Guid.Parse("cccccccc-0001-0000-0000-000000000000");

            modelBuilder.Entity<Kampagne>().HasData(
                new
                {
                    KampagneId = kampagne1Id,
                    Navn = "Sommerkampagne fysioterapi",
                    Aktiv = true,
                    GaeldendeBehandlingstyper = new List<BehandlingsType>
                    {
            BehandlingsType.Fysioterapi
                    }
                }
            );

            // 3. Owned type seed: Periode
            modelBuilder.Entity<Kampagne>().OwnsOne(k => k.Periode).HasData(
                new
                {
                    KampagneId = kampagne1Id,
                    StartDato = new DateOnly(2026, 6, 1),
                    SlutDato = new DateOnly(2026, 6, 30)
                }
            );

            // 4. Owned type seed: Rabatprocent
            modelBuilder.Entity<Kampagne>().OwnsOne(k => k.Rabatprocent).HasData(
                new
                {
                    KampagneId = kampagne1Id,
                    Value = 20m
                }
            );


        }
    }
}