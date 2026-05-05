using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Tests
{
    public class BedsteRabatBeregnerTests
    {
        [Fact]
        public async Task BeregnBedsteRabat_FlereRelevanteRabatter_VaelgerLavestePris()
        {
            var beregner = new BedsteRabatBeregner(
            [
                new LoyalitetsRabatBeregner(),
                new FoedselsdagsRabatBeregner(),
                new KampagneRabatBeregner()
            ]);

            var context = new RabatBeregningContext(
                PrisUdenRabat: new Penge(1000),
                BookingDato: new DateOnly(2026, 6, 15),
                KundeFoedselsdato: new DateOnly(1990, 6, 20),
                LoyalitetsNiveau: LoyalitetsNiveau.Guld,
                FoedselsdagsrabatBrugt: false,
                Behandlingstyper: [BehandlingsType.Sportsmassage],
                AktivKampagner:
                [
                    new Kampagne(
                        kampagneId: Guid.NewGuid(),
                        navn: "Sommerkampagne",
                        periode: new KampagnePeriode(
                            startDato: new DateOnly(2026, 6, 1),
                            slutDato: new DateOnly(2026, 6, 30)),
                        rabatprocent: new RabatProcent(20),
                        gaeldendeBehandlingstyper: [BehandlingsType.Sportsmassage])
                ]);

            var resultat = beregner.BeregnBedsteRabat(context);

            Assert.Equal(RabatType.Foedselsdag, resultat.RabatType);
            Assert.Equal(25, resultat.RabatProcent.Value);
            Assert.Equal(750, resultat.PrisMedRabat.Belob);
        }

        [Fact]
        public async Task BeregnBedsteRabat_UdenStrategier_ReturnererIngenRabat()
        {
            var beregner = new BedsteRabatBeregner([]);
            var context = new RabatBeregningContext(
                PrisUdenRabat: new Penge(1000),
                BookingDato: new DateOnly(2026, 6, 15),
                KundeFoedselsdato: new DateOnly(1990, 6, 20),
                LoyalitetsNiveau: LoyalitetsNiveau.Guld,
                FoedselsdagsrabatBrugt: false,
                Behandlingstyper: [],
                AktivKampagner: []);

            var resultat = beregner.BeregnBedsteRabat(context);

            Assert.Equal(RabatType.Ingen, resultat.RabatType);
            Assert.Equal(0, resultat.RabatProcent.Value);
            Assert.Equal(1000, resultat.PrisMedRabat.Belob);
        }
    }
}
