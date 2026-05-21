namespace BookRight.UseCases.Commands.Kunde
{
    public record OpretKundeCommand(
        string Fornavn,
        string Efternavn,
        string Email,
        string Telefon,
        DateOnly Fødselsdato,
        string Adresse,
        string Helbredsnotater,
        Guid ForetrukkenBehandlerID
    );
}
