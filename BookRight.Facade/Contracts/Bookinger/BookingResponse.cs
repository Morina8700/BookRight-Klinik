namespace BookRight.Facade.Contracts.Bookinger
{
    // Svar som sendes tilbage efter forsøg på at oprette en booking
    public class BookingResponse
    {
        public bool Success { get; set; }
        public decimal PrisUdenRabat { get; set; }
        public decimal PrisMedRabat { get; set; }
        public string? AnvendtRabatType { get; set; }
        public string? Besked { get; set; }
    }
}