using BookRight.Domain.Interfaces;

namespace BookRight.UseCases.Queries.Klinik;

public class HentAlleKlinikkerHandler
{
    private readonly IKlinikRepository _klinikRepository;

    public HentAlleKlinikkerHandler(IKlinikRepository klinikRepository)
    {
        _klinikRepository = klinikRepository;
    }

    public async Task<IReadOnlyList<KlinikListPost>> HandleAsync()
    {
        var klinikker = await _klinikRepository.HentAlleAsync();

        return klinikker.Select(k => new KlinikListPost(
            k.KlinikId,
            k.Navn ?? string.Empty,
            k.Adresse ?? string.Empty,
            k.AntalRum
        )).ToList();
    }
}