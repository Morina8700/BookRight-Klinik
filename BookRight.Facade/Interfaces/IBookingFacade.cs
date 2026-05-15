using BookRight.Facade.Contracts.Bookinger;
using BookRight.UseCases.DTOs;

namespace BookRight.Facade.Interfaces
{
    // Definerer de bookingfunktioner som UI/API kan kalde
    public interface IBookingFacade
    {
        Task<BookingResponse> OpretBookingAsync(OpretBookingRequest request);
        Task<IEnumerable<KlinikDto>> HentAlleKlinikkerAsync();
        Task<IEnumerable<BehandlerDto>> HentAlleBehandlereAsync();
        Task<IEnumerable<BehandlingstypeDto>> HentAlleBehandlingstyperAsync();

        //Query
        Task<List<BookingKalenderDto>> HentBookingerForDatoAsync(DateOnly dato);



        //Booking Status

        Task<bool> AflysBookingAsync(Guid bookingId);
        Task<bool> AfslutBookingAsync(Guid bookingId);
        Task<bool>MarkerAnkommetAsync(Guid bookingId);
        Task<bool> MarkerNoShowAsync(Guid bookingId);
    }
}
