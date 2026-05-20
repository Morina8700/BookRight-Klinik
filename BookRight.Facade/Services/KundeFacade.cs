using BookRight.Facade.Contracts.Kunder;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Commands.Kunde;
using BookRight.UseCases.Queries.Kunde;

namespace BookRight.Facade.Services;

public class KundeFacade : IKundeFacade
{
    private readonly OpretKundeHandler _opretKundeHandler;
    private readonly HentAlleKunderHandler _hentAlleKunderHandler;

    public KundeFacade(
        OpretKundeHandler opretKundeHandler,
        HentAlleKunderHandler hentAlleKunderHandler)
    {
        _opretKundeHandler = opretKundeHandler;
        _hentAlleKunderHandler = hentAlleKunderHandler;
    }

    public async Task<Guid> OpretKundeAsync(OpretKundeRequest request)
    {
        var command = new OpretKundeCommand(
            request.Fornavn,
            request.Efternavn,
            request.Email,
            request.Telefon,
            request.Fødselsdato,
            request.Adresse,
            request.Helbredsnotater,
            request.ForetrukkenBehandlerId);

        return await _opretKundeHandler.HandleAsync(command);
    }

    public async Task<IReadOnlyList<KundeDto>> HentAlleKunderAsync()
    {
        // 1. Use case returnerer KundeListPost (UseCases-lag)
        var kunder = await _hentAlleKunderHandler.HandleAsync();

        // 2. Facade mapper til KundeDto (Facade-lag / UI-kontrakt)
        return kunder.Select(k => new KundeDto
        {
            KundeId = k.KundeId,
            FuldeNavn = k.FuldeNavn,
            Email = k.Email,
            Telefon = k.Telefon,
            LoyalitetsNiveau = k.LoyalitetsNiveau
        }).ToList();
    }
}