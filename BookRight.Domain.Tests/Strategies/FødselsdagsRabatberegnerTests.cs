using System.Reflection;
using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.Interfaces;
using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Domain.ValueObjects;
using Moq;

namespace BookRight.Domain.Tests.Strategies;

public class FoedselsdagsRabatberegnerTests
{
    [Fact]
    public void FødselsdagsMånedBrugt_Returnerer25procent()
    {
        //Arrange
        var beregner = new FoedselsdagsRabatBeregner();
        var rabat = new RabatResultat(new Penge(1000));

        var context = new RabatBeregningContext(
            PrisUdenRabat: new Penge(1000),
            BookingDato: new DateOnly(2026, 5, 26),
            KundeFoedselsdato: new DateOnly(1993, 5, 3),
            LoyalitetsNiveau: LoyalitetsNiveau.Ingen,
            FoedselsdagsrabatBrugt: false,
            Behandlingstyper: [],
            AktivKampagner: []
            );


        //Act & Assert
        beregner.BeregnRabat(context, rabat);

        Assert.Equal(RabatType.Fødselsdag, rabat.RabatType);
        Assert.Equal(25, rabat.RabatProcent.Value);
        Assert.Equal(750, rabat.PrisMedRabat.Belob);
    }

    [Fact]
    public void BeregnRabat_FoedselsdagsrabatErBrugt_ReturnererIngenRabat()
    {
        //Arrange
        var beregner = new FoedselsdagsRabatBeregner();
        var rabat = new RabatResultat(new Penge(1000));

        var context = new RabatBeregningContext(
            PrisUdenRabat: new Penge(1000),
            BookingDato: new DateOnly(2026, 5, 26),
            KundeFoedselsdato: new DateOnly(1993, 5, 3),
            LoyalitetsNiveau: LoyalitetsNiveau.Ingen,
            FoedselsdagsrabatBrugt: true,
            Behandlingstyper: [],
            AktivKampagner: []
            );


        //Act & Assert
        beregner.BeregnRabat(context, rabat);

        Assert.Equal(RabatType.Ingen, rabat.RabatType);
        Assert.Equal(0, rabat.RabatProcent.Value);
        Assert.Equal(1000, rabat.PrisMedRabat.Belob);
    }

    [Fact]
    public void BeregnRabat_FoedselsdagsrabatErIkkeBrugt_ReturnererIngenRabat()
    {
        //Arrange
        var beregner = new FoedselsdagsRabatBeregner();
        var rabat = new RabatResultat(new Penge(1000));

        var context = new RabatBeregningContext(
            PrisUdenRabat: new Penge(1000),
            BookingDato: new DateOnly(2026, 5, 26),
            KundeFoedselsdato: new DateOnly(1993, 1, 3),
            LoyalitetsNiveau: LoyalitetsNiveau.Ingen,
            FoedselsdagsrabatBrugt: false,
            Behandlingstyper: [],
            AktivKampagner: []
            );


        //Act & Assert
        beregner.BeregnRabat(context, rabat);

        Assert.Equal(RabatType.Ingen, rabat.RabatType);
        Assert.Equal(1000, rabat.PrisMedRabat.Belob);
    }
}
