using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Facade.Contracts.Behandler
{
    public enum AutorisationsTypeDTO // Vi har skabt denne DTO for ikke at leak vores domæne enums direkte i vores system.
    {
        Fysioterapeut,
        Massør,
        Akupunktør,
        Kostvejleder
    }
}
