using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Tests
{
    public class KampagneRabatBeregnerTests
    {
        [Fact]
        public void BeregnRabat_AktivKampagne_Returnerer20Procent()
        {
            var beregner = new KampagneRabatBeregner();
            var kampagne = OpretKampagne(20);
            var resultat = new RabatResultat(new Penge(1000));
            var context = OpretContext([kampagne]);

            beregner.BeregnRabat(context, resultat);

            Assert.NotEqual(Guid.Empty, kampagne.KampagneId);
            Assert.Equal(RabatType.Kampagne, resultat.RabatType);
            Assert.Equal(20, resultat.RabatProcent.Value);
            Assert.Equal(1000, resultat.PrisUdenRabat.Belob);
            Assert.Equal(800, resultat.PrisMedRabat.Belob);
        }

        [Fact]
        public void BeregnRabat_InaktivKampagne_ReturnererIngenRabat()
        {
            var beregner = new KampagneRabatBeregner();
            var kampagne = OpretKampagne(20, aktiv: false);
            var resultat = new RabatResultat(new Penge(1000));
            var context = OpretContext([kampagne]);

            beregner.BeregnRabat(context, resultat);

            Assert.Equal(RabatType.Ingen, resultat.RabatType);
            Assert.Equal(0, resultat.RabatProcent.Value);
            Assert.Equal(1000, resultat.PrisMedRabat.Belob);
        }

        [Fact]
        public void BeregnRabat_FlereKampagner_VaelgerHoejesteRabat()
        {
            var beregner = new KampagneRabatBeregner();
            var kampagneMed10Procent = OpretKampagne(10);
            var kampagneMed30Procent = OpretKampagne(30);
            var resultat = new RabatResultat(new Penge(1000));
            var context = OpretContext([kampagneMed10Procent, kampagneMed30Procent]);

            beregner.BeregnRabat(context, resultat);

            Assert.Equal(RabatType.Kampagne, resultat.RabatType);
            Assert.Equal(30, resultat.RabatProcent.Value);
            Assert.Equal(700, resultat.PrisMedRabat.Belob);
        }

        private static Kampagne OpretKampagne(decimal rabatProcent, bool aktiv = true)
        {
            return new Kampagne(
                kampagneId: Guid.NewGuid(),
                navn: "Testkampagne",
                periode: new KampagnePeriode(
                    startDato: new DateOnly(2026, 4, 1),
                    slutDato: new DateOnly(2026, 6, 30)),
                rabatprocent: new RabatProcent(rabatProcent),
                gaeldendeBehandlingstyper: [BehandlingsType.Sportsmassage],
                aktiv: aktiv);
        }

        private static RabatBeregningContext OpretContext(IReadOnlyCollection<Kampagne> kampagner)
        {
            return new RabatBeregningContext(
                PrisUdenRabat: new Penge(1000),
                BookingDato: new DateOnly(2026, 5, 1),
                KundeFoedselsdato: new DateOnly(1998, 1, 10),
                LoyalitetsNiveau: LoyalitetsNiveau.Ingen,
                FoedselsdagsrabatBrugt: false,
                Behandlingstyper: [BehandlingsType.Sportsmassage],
                AktivKampagner: kampagner);
        }
    }
}
