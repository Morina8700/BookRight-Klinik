using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Strategies.Rabatberegner
{
    public record RabatResultat(
        string RabatNavn,
        decimal RabatProcent,
        decimal PrisUdenRabat,
        decimal PrisMedRabat


        );
}
