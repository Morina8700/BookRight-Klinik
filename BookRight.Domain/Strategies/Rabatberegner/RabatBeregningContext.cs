using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
namespace BookRight.Domain.Strategies.Rabatberegner
{
    // Context-klasse, der indeholder alle nødvendige informationer for at beregne rabatten
    // Så stratgierne ikke skal selv hente data fra databasen
    public record RabatBeregningContext(
    
        Penge PrisUdenRabat, // Pris uden rabat, som skal bruges til at beregne rabatten
        DateOnly BookingDato, // Datoen for bookingen, som kan bruges til at beregne rabatten
        DateOnly KundeFoedselsdato, // Kundens fødselsdato, som kan bruges til at beregne rabatten
        LoyalitetsNiveau LoyalitetsNiveau, // Kundens loyalitetsniveau, som kan bruges til at beregne rabatten
        bool FoedselsdagsrabatBrugt, // Angiver om fødselsdagsrabat allerede er brugt
        IReadOnlyCollection<BehandlingsType> Behandlingstyper, // Behandlingstyper, som kan bruges til at beregne rabatten
        IReadOnlyCollection<Kampagne> AktivKampagner // Aktive kampagner, som kan bruges til at beregne rabatten
    );

    
}
