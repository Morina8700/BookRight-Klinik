using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Tests
{
    public class LoyalitetsRabatBeregnerTests
    {
        [Fact]
        public void BeregnRabat_GuldKunde_15Procent()
        {
            var beregner = new LoyalitetsRabatBeregner();

            var context = new RabatBeregningContext(
                PrisUdenRabat: new Penge(1000),
                BookingDato: new DateOnly(2026, 6, 1),
                KundeFoedselsdato: new DateOnly(1980, 1, 1),
                LoyalitetsNiveau: LoyalitetsNiveau.Guld,
                FoedselsdagsrabatBrugt: false,
                Behandlingstyper: [],
                AktivKampagner: []




                );

            var resultat = beregner.BeregnRabat(context);

            Assert.Equal(RabatType.Loyalitet, resultat.RabatType);
            Assert.Equal(15, resultat.RabatProcent.Value);
            Assert.Equal(850, resultat.PrisMedRabat.Belob);

        }
    }
}
