namespace BookRight.Domain.ValueObjects
{
    public record RabatProcent
    {
        public decimal Value { get; init; }

        public RabatProcent(decimal value)
        {
            if (value < 0 || value > 100)
            
                throw new ArgumentOutOfRangeException(nameof(value), "Rabatprocent skal være mellem 0 og 100.");
            
            Value = value;
        }

        private RabatProcent() // For EF Core
            {
        }


    }
}
