namespace BookRight.UseCases.Queries.Behandler.Behandlingstype;

public record BehandlingstypeListPost(
    Guid BehandlingstypeId,
    string Navn,
    decimal Pris,
    int VarighedMinutter,
    string KrævetAutorisationsType
);