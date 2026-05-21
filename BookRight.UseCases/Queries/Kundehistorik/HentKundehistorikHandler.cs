using BookRight.Domain.Enums;

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

        var bookingerForKunde = await _kundehistorikQueryRepository.HentForKundeAsync(kundeId);
        return bookingerForKunde 
        .Where(booking => booking.Status == BookingStatus.Afsluttet ||
                    booking.Status == BookingStatus.Aflyst ||
                    booking.Status == BookingStatus.NoShow)
        .OrderByDescending(booking => booking.StartTid);
    }
}