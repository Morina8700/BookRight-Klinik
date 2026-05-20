namespace BookRight.Domain.ValueObjects
{
    public record Penge
    {
        public decimal Belob { get; init; }

        public Penge(decimal belob)
        {
            if (belob < 0)
                throw new ArgumentOutOfRangeException(nameof(belob), "Beløb kan ikke være negativt.");
            Belob = decimal.Round(belob, 2);
        }

        public Penge FratraekRabat(RabatProcent rabat)
        {
            var nyPris = Belob * (1 - rabat.Value / 100);
            return new Penge(nyPris);
        }
    }
}
