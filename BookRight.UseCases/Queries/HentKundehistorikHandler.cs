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

        public async Task<IEnumerable<KundehistorikPost>> HandleAsync(Guid kundeId)
        {
            return await _bookingRepository.HentKundehistorikAsync(kundeId);
        }
    }
}
