namespace BookRight.UseCases.Queries.Kundehistorik;

public interface IKundehistorikQueryRepository
{
    Task<IEnumerable<KundehistorikPost>> HentForKundeAsync(Guid kundeId);
}