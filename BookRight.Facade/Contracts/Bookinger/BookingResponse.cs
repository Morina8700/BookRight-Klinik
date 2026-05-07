namespace BookRight.Facade.Contracts.Bookinger
{
    public class BookingResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";

        public decimal PrisUdenRabat { get; set; }
        public decimal PrisMedRabat { get; set; }
        public string? AnvendtRabatType { get; set; }
    }
}