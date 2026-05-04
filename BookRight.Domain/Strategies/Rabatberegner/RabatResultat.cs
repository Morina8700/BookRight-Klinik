using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Strategies.Rabatberegner
{
    public record RabatResultat(
        RabatType RabatType,
        RabatProcent RabatProcent,
        Penge PrisUdenRabat,
        Penge PrisMedRabat
        );
}
