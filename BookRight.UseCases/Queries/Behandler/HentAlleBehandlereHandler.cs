using BookRight.Domain.Interfaces;

namespace BookRight.UseCases.Queries.Behandler;

public class HentAlleBehandlereHandler
{
    private readonly IBehandlerRepository _behandlerRepository;

    public HentAlleBehandlereHandler(IBehandlerRepository behandlerRepository)
    {
        _behandlerRepository = behandlerRepository;
    }

    public async Task<IReadOnlyList<BehandlerListPost>> HandleAsync()
    {
        var behandlere = await _behandlerRepository.HentAlleAsync();

        return behandlere.Select(b => new BehandlerListPost(
            b.BehandlerId,
            b.Fornavn ?? string.Empty,
            b.Efternavn ?? string.Empty,
            b.Klinikker.Select(k => k.KlinikId).ToList(),
            b.Behandlingstyper.Select(bt => bt.BehandlingstypeId).ToList()
        )).ToList();
    }
}