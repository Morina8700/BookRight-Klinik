using BookRight.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Strategies.Rabatberegner
{
    public record RabatBeregningContext(
    
        decimal PrisUdenRabat,
        DateOnly BookingDato,
        DateOnly KundeFoedselsdato,
        LoyalitetsNiveau LoyalitetsNiveau,
        bool FoedselsdagsrabatBrugt,
        IReadOnlyCollection<string> Behandlingstyper,
        IReadOnlyCollection<Kampagne> AktivKampagner
    );

    
}
