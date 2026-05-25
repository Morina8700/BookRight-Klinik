using BookRight.Domain.Interfaces;

namespace BookRight.UseCases.Queries.Behandler.Behandlingstype;

public class HentAlleBehandlingstyperHandler
{
    private readonly IBehandlingstypeRepository _behandlingstypeRepository;

    public HentAlleBehandlingstyperHandler(IBehandlingstypeRepository behandlingstypeRepository)
    {
        _behandlingstypeRepository = behandlingstypeRepository;
    }

    public async Task<IReadOnlyList<BehandlingstypeListPost>> HandleAsync()
    {
        var typer = await _behandlingstypeRepository.HentAlleAsync();

        return typer.Select(b => new BehandlingstypeListPost(
            b.BehandlingstypeId,
            b.Navn ?? string.Empty,
            b.Pris,
            b.VarighedMinutter,
            b.KrævetAutorisationsType.ToString()
        )).ToList();
    }
}