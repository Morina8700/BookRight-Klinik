using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Contracts.Kunder
{
    public class KundeDto
    {
        public Guid KundeId { get; set; }

        public string FuldeNavn { get; set; }

        public string Email { get; set; }

        public string Telefon { get; set; }

        public string LoyalitetsNiveau { get; set; }
    }
}
