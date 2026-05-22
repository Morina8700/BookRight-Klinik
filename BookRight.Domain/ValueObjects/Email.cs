namespace BookRight.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; init; }

        public Email (string  value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email må ikke være tom.");

            if (!value.Contains("@"))
                throw new ArgumentException("Email er ikke gyldig.");

            Value = value;
        }
    }
}
