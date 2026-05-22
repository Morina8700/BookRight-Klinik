using BookRight.Domain.Interfaces;

namespace BookRight.UseCases.Queries.Kunde;

public class HentAlleKunderHandler
{
    private readonly IKundeRepository _kundeRepository;

    public HentAlleKunderHandler(IKundeRepository kundeRepository)
    {
        _kundeRepository = kundeRepository;
    }

    public async Task<IReadOnlyList<KundeListPost>> HandleAsync()
    {
        var kunder = await _kundeRepository.HentAlleAsync();

        return kunder.Select(k => new KundeListPost(
            k.KundeId,
            $"{k.Fornavn} {k.Efternavn}",
            k.Email.Value,
            k.Telefon.Value,
            k.loyalitetsNiveau.ToString()
        )).ToList();
    }
}