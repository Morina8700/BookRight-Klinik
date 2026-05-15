using BookRight.Domain.Interfaces;
using BookRight.Domain.Models;

namespace BookRight.UseCases.Queries
{
    public class HentKundehistorikHandler
    {
        private readonly IBookingRepository _bookingRepository;

        public HentKundehistorikHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        // Henter historik for den valgte kunde
        public async Task<IEnumerable<KundehistorikPost>> HandleAsync(Guid kundeId)
        {
            if (kundeId == Guid.Empty) throw new ArgumentException("KundeId må ikke være tomt");

            return await _bookingRepository.HentKundehistorikAsync(kundeId);
        }
    }
}
