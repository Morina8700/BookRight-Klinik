using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Strategies.Rabatberegner
{
    public interface IRabatBeregner
    {
        RabatResultat BeregnRabat(RabatBeregningContext context);
    }
}
