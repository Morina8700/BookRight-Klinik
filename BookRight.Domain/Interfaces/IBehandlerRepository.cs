using BookRight.Domain.Aggregates;

namespace BookRight.Domain.Interfaces
{
    public interface IBehandlerRepository
    {
        Task<Behandler> HentMedDetaljerAsync(Guid behandlerId);
        Task<IEnumerable<Behandler>> HentAlleAsync();
    }
}