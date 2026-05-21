using BookRight.Domain.Aggregates;

namespace BookRight.Domain.Interfaces
{
    public interface IKampagneRepository
    {
        Task<IReadOnlyCollection<Kampagne>> HentAktiveKampagnerAsync(DateOnly dato);
    }
}
