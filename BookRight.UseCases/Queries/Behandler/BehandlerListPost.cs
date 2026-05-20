namespace BookRight.UseCases.Queries.Behandler;

public record BehandlerListPost(
    Guid BehandlerId,
    string Fornavn,
    string Efternavn,
    IReadOnlyList<Guid> KlinikIds,
    IReadOnlyList<Guid> BehandlingstypeIds
);