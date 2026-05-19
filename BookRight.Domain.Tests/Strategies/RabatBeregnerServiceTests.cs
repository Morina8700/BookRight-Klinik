using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.Strategies.Rabatberegner;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Tests.Strategies
{
    public class RabatBeregnerServiceTests
    {
        [Fact]
        public async Task BeregnBedsteRabatAsync_FlereRabatter_VaelgerBedsteRabat() //I denne test vælger vi alle de forskellige rabattyper
                                                                                    //og metoden skal vælge den der giver højst rabat.
                                                                                    // I dette eksempel er det Kampagne.
        {
            //Arrange
            var service = new RabatBeregnerService([
                new LoyalitetsRabatBeregner(),
                new FoedselsdagsRabatBeregner(),
                new KampagneRabatBeregner()

                ]);

            var kampagne = new Kampagne(
                kampagneId: Guid.NewGuid(),
                navn: "SOMMERKAMPAGNE 30% PÅ AKUPUNKTUR",
                periode: new KampagnePeriode(
                    startDato: new DateOnly(2026, 6, 1),
                    slutDato: new DateOnly(2026, 6, 30)
                    ),
                rabatprocent: new RabatProcent(30),
                gaeldendeBehandlingstyper: [BehandlingsType.Akupunktur],
                aktiv: true
                );

            // I vores context der vælger vi at bruge alle de forskellige rabatyper(Loyalitet, fødselsdag og kampagne)
            var context = new RabatBeregningContext(
                PrisUdenRabat: new Penge(1000),
                BookingDato: new DateOnly(2026, 6, 5),
                KundeFoedselsdato: new DateOnly(1993, 6, 1),
                LoyalitetsNiveau: LoyalitetsNiveau.Guld,
                FoedselsdagsrabatBrugt: false,
                Behandlingstyper: [BehandlingsType.Akupunktur],
                AktivKampagner: [kampagne]
                );

            var resultat = await service.BeregnBedsteRabatAsync( context );

            //Act & assert
            Assert.Equal(RabatType.Kampagne, resultat.RabatType);
            Assert.Equal(30, resultat.RabatProcent.Value);
            Assert.Equal(700, resultat.PrisMedRabat.Belob);

            //Hvis testen lyser grønt, så har den valgt den højeste rabat, som i dette tilfælde er 30%s sommerkampagne
            // Hvis vi ændrede i Linje 50, procenten til 25 som er fødselsdag rabat, så vil testen fejle, da den forventer de 30%.

        }


        [Fact]
        public async Task BeregnBedsteRabatAsync_IngenRabat_ReturnererIntet()
        {
            //Arrange
            var service = new RabatBeregnerService([
                new LoyalitetsRabatBeregner(),
                new FoedselsdagsRabatBeregner(),
                new KampagneRabatBeregner()

                ]);

            var context = new RabatBeregningContext(
                PrisUdenRabat: new Penge(1000),
                BookingDato: new DateOnly(2026, 7, 5),
                KundeFoedselsdato: new DateOnly(1993, 6, 1),
                LoyalitetsNiveau: LoyalitetsNiveau.Ingen,
                FoedselsdagsrabatBrugt: false,
                Behandlingstyper: [],
                AktivKampagner: []
                );

            var resultat = await service.BeregnBedsteRabatAsync(context);

            //Act & assert
            Assert.Equal(RabatType.Ingen, resultat.RabatType);
            Assert.Equal(0, resultat.RabatProcent.Value);
            Assert.Equal(1000, resultat.PrisMedRabat.Belob);

        }
    }
}
