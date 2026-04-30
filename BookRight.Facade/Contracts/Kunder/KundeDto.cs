using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Contracts.Kunder
{
    public class KundeDto
    {
        public Guid KundeId { get; set; }

        public string FuldeNavn { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Telefon { get; set; } = string.Empty;

        public string LoyalitetsNiveau { get; set; } = string.Empty;
    }
}
