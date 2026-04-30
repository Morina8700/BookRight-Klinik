using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Contracts.Kunder
{
    public class KundeResponse
    {
        public Guid KundeId { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; } = "";
    }
}
