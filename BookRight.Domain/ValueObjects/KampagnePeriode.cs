namespace BookRight.Domain.ValueObjects
{
    public record KampagnePeriode
    {
        public DateOnly StartDato { get; init; }
        public DateOnly SlutDato { get; init; }

        public KampagnePeriode(DateOnly startDato, DateOnly slutDato)
        {
            if (slutDato < startDato)
                throw new ArgumentException("Slutdato skal være efter startdato.");
            StartDato = startDato;
            SlutDato = slutDato;
        }

        public bool Indeholder(DateOnly dato)
        {
            return dato >= StartDato && dato <= SlutDato;
        }
        private KampagnePeriode() // For EF Core
        {
        }
    }
}
