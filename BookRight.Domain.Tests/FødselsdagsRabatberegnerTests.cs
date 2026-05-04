using BookRight.Domain.Enums;
using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Tests;

public class FoedselsdagsRabatberegnerTests
{
    [Fact]
    public void BeregnRabat_BookingIFoedselsmaaned_ReturnererRabat()
    {
        var beregner = new FoedselsdagsRabatBeregner();

        var context = new RabatBeregningContext(
            PrisUdenRabat: new Penge(1000),
            BookingDato: new DateOnly(2026, 6, 15),
            KundeFoedselsdato: new DateOnly(1990, 6, 20),
            LoyalitetsNiveau: LoyalitetsNiveau.Ingen,
            FoedselsdagsrabatBrugt: false,
            Behandlingstyper: [],
            AktivKampagner: []);

        var resultat = beregner.BeregnRabat(context);

        Assert.Equal(RabatType.Foedselsdag, resultat.RabatType);
        Assert.Equal(25, resultat.RabatProcent.Value);
        Assert.Equal(750, resultat.PrisMedRabat.Belob);
    }

    [Fact]
    public void BeregnRabat_BookingIkkeIFoedselsmaaned_ReturnererIngenRabat()
    {
        var beregner = new FoedselsdagsRabatBeregner();

        var context = new RabatBeregningContext(
            PrisUdenRabat: new Penge(1000),
            BookingDato: new DateOnly(2026, 7, 15),
            KundeFoedselsdato: new DateOnly(1990, 6, 20),
            LoyalitetsNiveau: LoyalitetsNiveau.Ingen,
            FoedselsdagsrabatBrugt: false,
            Behandlingstyper: [],
            AktivKampagner: []);

        var resultat = beregner.BeregnRabat(context);

        Assert.Equal(RabatType.Ingen, resultat.RabatType);
        Assert.Equal(0, resultat.RabatProcent.Value);
        Assert.Equal(1000, resultat.PrisUdenRabat.Belob);
        Assert.Equal(1000, resultat.PrisMedRabat.Belob);
    }

    [Fact]
    public void BeregnRabat_FoedselsdagsrabatAlleredeBrugt_ReturnererIngenRabat()
    {
        var beregner = new FoedselsdagsRabatBeregner();

        var context = new RabatBeregningContext(
            PrisUdenRabat: new Penge(1000),
            BookingDato: new DateOnly(2026, 6, 15),
            KundeFoedselsdato: new DateOnly(1990, 6, 20),
            LoyalitetsNiveau: LoyalitetsNiveau.Ingen,
            FoedselsdagsrabatBrugt: true,
            Behandlingstyper: [],
            AktivKampagner: []);

        var resultat = beregner.BeregnRabat(context);

        Assert.Equal(RabatType.Ingen, resultat.RabatType);
        Assert.Equal(0, resultat.RabatProcent.Value);
        Assert.Equal(1000, resultat.PrisMedRabat.Belob);
    }
}
