using BookRight.Domain.Aggregates;

namespace BookRight.Domain.Interfaces
{
    public interface IBehandlingstypeRepository
    {
        Task<Behandlingstype> HentAsync(Guid behandlingstypeId);
        Task<IEnumerable<Behandlingstype>> HentAlleAsync();
    }
}