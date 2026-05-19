using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Tests.Strategies
{
    public class KampagneRabatBeregnerTests
    {
        [Fact]

        public void BeregnRabat_AktivKampagneIndenForPeriodeOgBehandling_Giver30Procent()
        {
            // Arrange
            var beregner = new KampagneRabatBeregner();
            var resultat = new RabatResultat(new Penge(1000));

            var kampagne = OpretKampagne(
                navn: "EFTERÅRS-KAMPAGNE 30% PÅ FYSIOTERAPI",
                rabatProcent: 30,
                startDato: new DateOnly(2026, 10, 1),
                slutDato: new DateOnly(2026, 10, 31),
                behandlingsType: BehandlingsType.Fysioterapi,
                aktiv: true);

            var context = OpretContext(
                bookingDato: new DateOnly(2026, 10, 17),
                behandlingstype: BehandlingsType.Fysioterapi,
                kampagner: [kampagne]);


            beregner.BeregnRabat(context, resultat);

            // Act & Assert

            Assert.Equal(RabatType.Kampagne, resultat.RabatType);
            Assert.Equal(30, resultat.RabatProcent.Value);
            Assert.Equal(700, resultat.PrisMedRabat.Belob);


        }


        [Fact]

        public void BeregnRabat_AktivKampagne_MenVælgesIkkeKorrekteBehanlingstype_GiverIngenRabat()
        {
            // Arrange
            var beregner = new KampagneRabatBeregner();
            var resultat = new RabatResultat(new Penge(1000));

            var kampagne = OpretKampagne(
                navn: "EFTERÅRS-KAMPAGNE 30% PÅ FYSIOTERAPI",
                rabatProcent: 30,
                startDato: new DateOnly(2026, 10, 1),
                slutDato: new DateOnly(2026, 10, 31),
                behandlingsType: BehandlingsType.Fysioterapi,
                aktiv: true);

            var context = OpretContext(
                bookingDato: new DateOnly(2026, 10, 17),
                behandlingstype: BehandlingsType.Sportsmassage,
                kampagner: [kampagne]);


            beregner.BeregnRabat(context, resultat);

            // Act & Assert

            Assert.Equal(RabatType.Ingen, resultat.RabatType);
            Assert.Equal(1000, resultat.PrisMedRabat.Belob);


        }

        [Fact]

        public void BeregnRabat_FlereAktiveKampagner_VælgerHøjestRabat()
        {
            // Arrange
            var beregner = new KampagneRabatBeregner();
            var resultat = new RabatResultat(new Penge(1000));

            var kampagne = OpretKampagne(
                navn: "EFTERÅRS-KAMPAGNE 30% PÅ FYSIOTERAPI",
                rabatProcent: 30,
                startDato: new DateOnly(2026, 10, 1),
                slutDato: new DateOnly(2026, 10, 31),
                behandlingsType: BehandlingsType.Fysioterapi,
                aktiv: true);

            var kampagne2 = OpretKampagne(
    navn: "EFTERÅRS-KAMPAGNE 20% PÅ AKUNPUKTUR",
    rabatProcent: 20,
    startDato: new DateOnly(2026, 10, 1),
    slutDato: new DateOnly(2026, 10, 31),
    behandlingsType: BehandlingsType.Akupunktur,
    aktiv: true);

            var kampagne3 = OpretKampagne(
navn: "EFTERÅRS-KAMPAGNE 40% PÅ SPORTSMASSAGE",
rabatProcent: 40,
startDato: new DateOnly(2026, 10, 1),
slutDato: new DateOnly(2026, 10, 31),
behandlingsType: BehandlingsType.Sportsmassage,
aktiv: true);

            var context = OpretContext(
                bookingDato: new DateOnly(2026, 10, 17),
                behandlingstype: BehandlingsType.Sportsmassage,
                kampagner: [kampagne3]);


            beregner.BeregnRabat(context, resultat);

            // Act & Assert

            Assert.Equal(RabatType.Kampagne, resultat.RabatType);
            Assert.Equal(40, resultat.RabatProcent.Value);
            Assert.Equal(600, resultat.PrisMedRabat.Belob);


        }





        private static Kampagne OpretKampagne(
            string navn,
            decimal rabatProcent,
            DateOnly startDato,
            DateOnly slutDato,
            BehandlingsType behandlingsType,
            bool aktiv


            )
        {
            return new Kampagne(
                kampagneId: Guid.NewGuid(),
                navn: navn,
                periode: new KampagnePeriode(startDato, slutDato),
                rabatprocent: new RabatProcent(rabatProcent),
                gaeldendeBehandlingstyper: [behandlingsType],
                aktiv: aktiv);

        }


        private static RabatBeregningContext OpretContext(
        DateOnly bookingDato,
        BehandlingsType behandlingstype,
        IReadOnlyCollection<Kampagne> kampagner)
        {
            return new RabatBeregningContext(
                PrisUdenRabat: new Penge(1000),
                BookingDato: bookingDato,
                KundeFoedselsdato: new DateOnly(1998, 1, 1),
                LoyalitetsNiveau: LoyalitetsNiveau.Ingen,
                FoedselsdagsrabatBrugt: false,
                Behandlingstyper: [behandlingstype],
                AktivKampagner: kampagner);
        }













    }

       
    }

