using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Strategies.Rabatberegner
{
    public record RabatResultat(
        RabatType RabatType, // Angiver hvilken type rabat der er beregnet
        RabatProcent RabatProcent, // Angiver hvor stor rabatten er i procent, som kan bruges til at vise rabatten til kunden
        Penge PrisUdenRabat, // Pris uden rabat, som kan bruges til at vise den oprindelige pris til kunden
        Penge PrisMedRabat // Pris med rabat, som kan bruges til at vise den endelige pris til kunden
        );
    

   



    
    
    

}
