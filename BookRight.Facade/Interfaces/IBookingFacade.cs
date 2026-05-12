using BookRight.Facade.Contracts.Bookinger;

namespace BookRight.Facade.Interfaces
{
    // Definerer de bookingfunktioner som UI/API kan kalde
    public interface IBookingFacade
    {
        Task<BookingResponse> OpretBookingAsync(OpretBookingRequest request);
        Task<IEnumerable<KlinikDto>> HentAlleKlinikkerAsync();
        Task<IEnumerable<BehandlerDto>> HentAlleBehandlereAsync();
        Task<IEnumerable<BehandlingstypeDto>> HentAlleBehandlingstyperAsync();
    }
}
