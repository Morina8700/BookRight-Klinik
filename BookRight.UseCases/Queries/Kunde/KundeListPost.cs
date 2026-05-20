namespace BookRight.UseCases.Queries.Kunde;

public record KundeListPost(
    Guid KundeId,
    string FuldeNavn,
    string Email,
    string Telefon,
    string LoyalitetsNiveau
);