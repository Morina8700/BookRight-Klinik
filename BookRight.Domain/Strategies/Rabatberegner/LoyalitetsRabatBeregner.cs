using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Strategies.Rabatberegner
{
    public class LoyalitetsRabatBeregner : IRabatBeregner
    {
        public RabatResultat BeregnRabat(RabatBeregningContext context)
        {
            var rabatProcent = context.LoyalitetsNiveau switch
            {
                LoyalitetsNiveau.Bronze => new RabatProcent(5),
                LoyalitetsNiveau.Sølv => new RabatProcent(10),
                LoyalitetsNiveau.Guld => new RabatProcent(15),
                _ => new RabatProcent(0)
            };

            var prisMedRabat = context.PrisUdenRabat.FratraekRabat(rabatProcent);

            return new RabatResultat(
                rabatProcent.Value == 0 ? RabatType.Ingen : RabatType.Loyalitet,
                rabatProcent,
                context.PrisUdenRabat,
                prisMedRabat
            );
        }
    }
}
