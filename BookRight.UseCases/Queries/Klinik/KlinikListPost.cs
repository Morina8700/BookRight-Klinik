namespace BookRight.UseCases.Queries.Klinik;

public record KlinikListPost(
    Guid KlinikId,
    string Navn,
    string Adresse,
    int AntalRum
);