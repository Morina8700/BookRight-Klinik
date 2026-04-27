using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Contracts.Kunder
{
    public class OpretKundeRequest
    {
        public string Fornavn { get; set; } = "";
        public string Efternavn { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefon { get; set; } = "";
        public DateOnly Fødselsdato { get; set; }
        public string Adresse { get; set; } = "";
        public string Helbredsnotater { get; set; } = "";
        public Guid ForetrukkenBehandlerId { get; set; }

    }
}
