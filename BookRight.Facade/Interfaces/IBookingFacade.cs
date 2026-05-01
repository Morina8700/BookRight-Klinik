using BookRight.Facade.Contracts.Bookinger;

namespace BookRight.Facade.Interfaces
{
    public interface IBookingFacade
    {
        Task<BookingResponse> OpretBookingAsync(OpretBookingRequest request);
        Task<IEnumerable<BehandlerDto>> HentAlleBehandlereAsync();
        Task<IEnumerable<KlinikDto>> HentAlleKlinikkerAsync();
        Task<IEnumerable<BehandlingstypeDto>> HentAlleBehandlingstyperAsync();
    }
}
