using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
namespace BookRight.Domain.Strategies.Rabatberegner
{
    public class FoedselsdagsRabatBeregner : IRabatBeregner
    {
        public void BeregnRabat(RabatBeregningContext context, RabatResultat resultat)
        {
            if(context.FoedselsdagsrabatBrugt || 
                context.KundeFoedselsdato.Month != context.BookingDato.Month)
            {
                return;
            }

            var rabatProcent = new RabatProcent(25);
            var prisMedRabat = context.PrisUdenRabat.FratraekRabat(rabatProcent);

            resultat.OpdaterHvisBedre(
                RabatType.Fødselsdag,
                rabatProcent,
                prisMedRabat

                );

        }
    }
}
