namespace BookRight.UseCases.Commands.Booking.OpretBooking
{
    public class OpretBookingResult
    {
        public bool Success { get; set; }
        public decimal PrisUdenRabat { get; set; }
        public decimal PrisMedRabat { get; set; }
        public string? AnvendtRabatType { get; set; }
        public decimal RabatProcent { get; set; }
    }
}
