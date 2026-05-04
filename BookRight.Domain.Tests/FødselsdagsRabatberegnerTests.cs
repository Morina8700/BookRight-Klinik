using BookRight.Domain.Enums;
using BookRight.Domain.Strategies;
using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Domain.ValueObjects;
namespace BookRight.Domain.Tests;

public class FoedselsdagsRabatberegnerTests
{
    [Fact]
    public void BeregnRabat_BookingIFoedselsMåned_ReturnererRabat()
    {
        var beregner = new FoedselsdagsRabatBeregner();

        var context = new RabatBeregningContext(
            PrisUdenRabat: new Penge(1000),
            BookingDato: new DateOnly(2026, 6, 15),
            KundeFoedselsdato: new DateOnly(1990, 6, 20),
            LoyalitetsNiveau: LoyalitetsNiveau.Ingen,
            FoedselsdagsrabatBrugt: false,
            Behandlingstyper: [],
            AktivKampagner: []
            );
    }

        [Fact]
        public void BeregnRabat_BookingIkkeIFoedselsMåned_ReturnererIngenRabat()
        {
            var beregner = new FoedselsdagsRabatBeregner();
            var context = new RabatBeregningContext(
                PrisUdenRabat: new Penge(1000),
                BookingDato: new DateOnly(2026, 7, 15),
                KundeFoedselsdato: new DateOnly(1990, 6, 20),
                LoyalitetsNiveau: LoyalitetsNiveau.Ingen,
                FoedselsdagsrabatBrugt: false,
                Behandlingstyper: [],
                AktivKampagner: []
                );

        var resultat = beregner.BeregnRabat(context);

        Assert.Equal(RabatType.Ingen, resultat.RabatType);
        Assert.Equal(0, resultat.RabatProcent.Value);
        Assert.Equal(1000, resultat.PrisUdenRabat.Belob);
    }
}
