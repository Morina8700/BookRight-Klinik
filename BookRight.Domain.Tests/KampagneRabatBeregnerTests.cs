using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Tests
{
    public class KampagneRabatBeregnerTests
    {
        public void BeregnRabat_AktivKampagne_20Procent()
        {
            var beregner = new KampagneRabatBeregner();

            var kampagne = new Kampagne(
              kampagneId: Guid.NewGuid(),
                navn: "Sommerkampagne",
                periode: new KampagnePeriode(
                    startDato: new DateOnly(2026, 4, 1),
                    slutDato: new DateOnly(2026, 6, 30)),
                rabatprocent: new RabatProcent(20),
                gaeldendeBehandlingstyper: [BehandlingsType.Sportsmassage, BehandlingsType.Fysioterapi],
                aktiv: true
                );



            var context = new RabatBeregningContext(
                PrisUdenRabat: new Penge(1000),
                BookingDato: new DateOnly(2026, 5, 1),
                KundeFoedselsdato: new DateOnly(1998, 1, 10),
                LoyalitetsNiveau: LoyalitetsNiveau.Ingen,
                FoedselsdagsrabatBrugt: false,
                Behandlingstyper: [BehandlingsType.Sportsmassage],
                AktivKampagner: [kampagne]
                );

            var resultat = beregner.BeregnRabat(context);

            Assert.NotEqual(Guid.Empty, kampagne.KampagneId);
            Assert.Equal(RabatType.Kampagne, resultat.RabatType);
            Assert.Equal(20, resultat.RabatProcent.Value);
            Assert.Equal(1000, resultat.PrisUdenRabat.Belob);
            Assert.Equal(800, resultat.PrisMedRabat.Belob);

        }
    }
}
