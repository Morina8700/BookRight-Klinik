using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Strategies.Rabatberegner
{
    public record RabatBeregningContext(
    
        Penge PrisUdenRabat,
        DateOnly BookingDato,
        DateOnly KundeFoedselsdato,
        LoyalitetsNiveau LoyalitetsNiveau,
        bool FoedselsdagsrabatBrugt,
        IReadOnlyCollection<BehandlingsType> Behandlingstyper,
        IReadOnlyCollection<Kampagne> AktivKampagner
    );

    
}
