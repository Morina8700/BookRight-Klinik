using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Strategies.Rabatberegner
{
    public class FoedselsdagsRabatBeregner : IRabatBeregner
    {
        public RabatResultat BeregnRabat(RabatBeregningContext context)
        {
            if(context.FoedselsdagsrabatBrugt || 
                context.KundeFoedselsdato.Month != context.BookingDato.Month)
            {
                var ingenRabat = new RabatProcent(0);
                    return new RabatResultat(
                        RabatType.Ingen,
                        ingenRabat,
                        context.PrisUdenRabat,
                        context.PrisUdenRabat
    
                        );
            }

            var rabatProcent = new RabatProcent(25);
            var prisMedRabat = context.PrisUdenRabat.FratraekRabat(rabatProcent);

            return new RabatResultat(
                RabatType.Fødselsdag,
                rabatProcent,
                context.PrisUdenRabat,
                prisMedRabat
                );

        }
    }
}
