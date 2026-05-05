using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Strategies.Rabatberegner
{
    // Interface for rabatberegner strategier
    // Alle rabattyper følger samme interface
    public interface IRabatBeregner
    {
        RabatResultat BeregnRabat(RabatBeregningContext context);
    }
}
