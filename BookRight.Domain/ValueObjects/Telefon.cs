using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.ValueObjects
{
    public class Telefon
    {
        public string Value { get; init; }

        public Telefon (string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Telefon må ikke være tom.");

            if (value.Length < 8)
                throw new ArgumentException("Telefonnummer skal mindst være 8 cifre.");
            if (!value.All(Char.IsDigit))
                throw new ArgumentException("Telefonnummer skal kun indeholde tal");

            Value = value;

        }
    }
}
