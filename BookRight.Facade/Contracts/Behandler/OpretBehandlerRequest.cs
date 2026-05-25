using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Contracts.Behandler
{
    public class OpretBehandlerRequest
    {
        public string Fornavn { get; set; } = "";
        public string Efternavn { get; set; } = "";
        public string Email { get; set; } = "";
        public string Telefon { get; set; } = "";
        public string AutorisationsNummer { get; set; } = "";
        public AutorisationsTypeDTO AutorisationsType { get; set; }
        public List<Guid> KlinikIds { get; set; } = new(); // Bruges i UI til at vise behandleren ved de klinikker, hvor behandleren arbejder
        public List<Guid> BehandlingstypeIds { get; set; } = new();
    }
}
