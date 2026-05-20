namespace BookRight.UseCases.Queries.Kundehistorik;

public class HentKundehistorikHandler
{
    private readonly IKundehistorikQueryRepository _kundehistorikQueryRepository;

    public HentKundehistorikHandler(IKundehistorikQueryRepository kundehistorikQueryRepository)
    {
        _kundehistorikQueryRepository = kundehistorikQueryRepository;
    }

    public async Task<IEnumerable<KundehistorikPost>> HandleAsync(Guid kundeId)
    {
        if (kundeId == Guid.Empty)
            throw new ArgumentException("KundeId må ikke være tomt");

        return await _kundehistorikQueryRepository.HentForKundeAsync(kundeId);
    }
}