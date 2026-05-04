using BookRight.Domain.Enums;
using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Tests
{
    public class LoyalitetsRabatBeregnerTests
    {
        [Fact]
        public void BeregnRabat_BronzeKunde_Returnerer5Procent()
        {
            var resultat = BeregnFor(LoyalitetsNiveau.Bronze);

            Assert.Equal(RabatType.Loyalitet, resultat.RabatType);
            Assert.Equal(5, resultat.RabatProcent.Value);
            Assert.Equal(950, resultat.PrisMedRabat.Belob);
        }

        [Fact]
        public void BeregnRabat_SolvKunde_Returnerer10Procent()
        {
            var resultat = BeregnFor(LoyalitetsNiveau.Sølv);

            Assert.Equal(RabatType.Loyalitet, resultat.RabatType);
            Assert.Equal(10, resultat.RabatProcent.Value);
            Assert.Equal(900, resultat.PrisMedRabat.Belob);
        }

        [Fact]
        public void BeregnRabat_GuldKunde_Returnerer15Procent()
        {
            var resultat = BeregnFor(LoyalitetsNiveau.Guld);

            Assert.Equal(RabatType.Loyalitet, resultat.RabatType);
            Assert.Equal(15, resultat.RabatProcent.Value);
            Assert.Equal(850, resultat.PrisMedRabat.Belob);
        }

        [Fact]
        public void BeregnRabat_IngenLoyalitet_ReturnererIngenRabat()
        {
            var resultat = BeregnFor(LoyalitetsNiveau.Ingen);

            Assert.Equal(RabatType.Ingen, resultat.RabatType);
            Assert.Equal(0, resultat.RabatProcent.Value);
            Assert.Equal(1000, resultat.PrisMedRabat.Belob);
        }

        private static RabatResultat BeregnFor(LoyalitetsNiveau loyalitetsNiveau)
        {
            var beregner = new LoyalitetsRabatBeregner();

            var context = new RabatBeregningContext(
                PrisUdenRabat: new Penge(1000),
                BookingDato: new DateOnly(2026, 6, 1),
                KundeFoedselsdato: new DateOnly(1980, 1, 1),
                LoyalitetsNiveau: loyalitetsNiveau,
                FoedselsdagsrabatBrugt: false,
                Behandlingstyper: [],
                AktivKampagner: []);

            return beregner.BeregnRabat(context);
        }
    }
}
