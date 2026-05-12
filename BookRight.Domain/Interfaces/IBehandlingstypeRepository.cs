using BookRight.Domain.Aggregates;

namespace BookRight.Domain.Interfaces
{
    public interface IBehandlingstypeRepository
    {
        Task<Behandlingstype?> HentEfterIdAsync(Guid behandlingstypeId);
        Task<IEnumerable<Behandlingstype>> HentAlleAsync();
    }
}