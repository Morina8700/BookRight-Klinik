using BookRight.Domain.Enums;
using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Tests.Strategies
{
    public class LoyalitetsRabatBeregnerTests
    {
        [Fact]
        public void BeregnRabat_BronzeKunde_Returnerer5Procent()
        {
            //Arrange
            var beregner = new LoyalitetsRabatBeregner();
            var resultat = new RabatResultat(new Penge(1000));

            var context = new RabatBeregningContext(
                PrisUdenRabat: new Penge(1000),
                BookingDato: new DateOnly(2026, 5, 26),
                KundeFoedselsdato: new DateOnly(1993, 1, 26),
                LoyalitetsNiveau: LoyalitetsNiveau.Bronze,
                FoedselsdagsrabatBrugt: false,
                Behandlingstyper: [],
                AktivKampagner: []
                );

            beregner.BeregnRabat(context, resultat);

            //Act & Assert
            Assert.Equal(RabatType.Loyalitet, resultat.RabatType);
            Assert.Equal(5, resultat.RabatProcent.Value);
            Assert.Equal(950, resultat.PrisMedRabat.Belob);
        }

        [Fact]
        public void BeregnRabat_SoelvKunde_Returnerer10Procent()
        {
            //Arrange
            var beregner = new LoyalitetsRabatBeregner();
            var resultat = new RabatResultat(new Penge(1000));

            var context = new RabatBeregningContext(
                PrisUdenRabat: new Penge(1000),
                BookingDato: new DateOnly(2026, 5, 26),
                KundeFoedselsdato: new DateOnly(1993, 1, 26),
                LoyalitetsNiveau: LoyalitetsNiveau.Sølv,
                FoedselsdagsrabatBrugt: false,
                Behandlingstyper: [],
                AktivKampagner: []
                );

            beregner.BeregnRabat(context, resultat);

            //Act & Assert
            Assert.Equal(RabatType.Loyalitet, resultat.RabatType);
            Assert.Equal(10, resultat.RabatProcent.Value);
            Assert.Equal(900, resultat.PrisMedRabat.Belob);
        }

        [Fact]
        public void BeregnRabat_GuldKunde_Returnerer15Procent()
        {
            //Arrange
            var beregner = new LoyalitetsRabatBeregner();
            var resultat = new RabatResultat(new Penge(1000));

            var context = new RabatBeregningContext(
                PrisUdenRabat: new Penge(1000),
                BookingDato: new DateOnly(2026, 5, 26),
                KundeFoedselsdato: new DateOnly(1993, 1, 26),
                LoyalitetsNiveau: LoyalitetsNiveau.Guld,
                FoedselsdagsrabatBrugt: false,
                Behandlingstyper: [],
                AktivKampagner: []
                );

            beregner.BeregnRabat(context, resultat);

            //Act & Assert
            Assert.Equal(RabatType.Loyalitet, resultat.RabatType);
            Assert.Equal(15, resultat.RabatProcent.Value);
            Assert.Equal(850, resultat.PrisMedRabat.Belob);
        }

        [Fact]
        public void BeregnRabat_UdenLoyalitetKunde_ReturnererIngenRabat()
        {
            //Arrange
            var beregner = new LoyalitetsRabatBeregner();
            var resultat = new RabatResultat(new Penge(1000));

            var context = new RabatBeregningContext(
                PrisUdenRabat: new Penge(1000),
                BookingDato: new DateOnly(2026, 5, 26),
                KundeFoedselsdato: new DateOnly(1993, 1, 26),
                LoyalitetsNiveau: LoyalitetsNiveau.Ingen,
                FoedselsdagsrabatBrugt: false,
                Behandlingstyper: [],
                AktivKampagner: []
                );

            beregner.BeregnRabat(context, resultat);

            //Act & Assert
            Assert.Equal(0, resultat.RabatProcent.Value);
            Assert.Equal(1000, resultat.PrisMedRabat.Belob);
        }


    }
}
