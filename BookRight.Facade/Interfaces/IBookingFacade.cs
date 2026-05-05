using BookRight.Facade.Contracts.Bookinger;
using BookRight.Facade.Contracts.Kunder;

namespace BookRight.Facade.Interfaces
{
    public interface IBookingFacade
    {
        Task<BookingResponse> OpretBookingAsync(OpretBookingRequest request);
        Task<IEnumerable<KundeDto>> HentAlleKunderAsync();
        Task<IEnumerable<BehandlerDto>> HentAlleBehandlereAsync();
        Task<IEnumerable<KlinikDto>> HentAlleKlinikkerAsync();
        Task<IEnumerable<BehandlingstypeDto>> HentAlleBehandlingstyperAsync();
    }
}
