using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
namespace BookRight.Domain.Strategies.Rabatberegner
{
    public class LoyalitetsRabatBeregner : IRabatBeregner
    {
        public void BeregnRabat(RabatBeregningContext context, RabatResultat resultat)
        {
            // Loyalitetsstrategien kigger på kundens loyalitetsniveau
            // og giver en rabat baseret på det niveau.
            // For eksempel: Sølvmedlemmer får 10% rabat, guldmedlemmer får 15% rabat, og bronzemedlemmer får 5% rabat.
            var rabatProcent = context.LoyalitetsNiveau switch
            {
                LoyalitetsNiveau.Bronze => new RabatProcent(5),
                LoyalitetsNiveau.Sølv => new RabatProcent(10),
                LoyalitetsNiveau.Guld => new RabatProcent(15),
                _ => new RabatProcent(0)
            };

            if (rabatProcent.Value == 0)
                return;

            var prisMedRabat = context.PrisUdenRabat.FratraekRabat(rabatProcent);

            resultat.OpdaterHvisBedre(
                RabatType.Loyalitet,
                rabatProcent,
                prisMedRabat

                );


           
        }
    }
}
