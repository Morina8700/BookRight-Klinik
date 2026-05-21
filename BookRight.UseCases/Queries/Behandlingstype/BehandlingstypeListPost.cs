namespace BookRight.UseCases.Queries.Behandlingstype;

public record BehandlingstypeListPost(
    Guid BehandlingstypeId,
    string Navn,
    decimal Pris,
    int VarighedMinutter
);