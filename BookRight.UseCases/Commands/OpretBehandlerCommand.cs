using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Commands
{
    public record OpretBehandlerCommand(
     Guid BehandlerId,
     string Fornavn,
     string Efternavn,
     string Email,
     string Telefon,
     String AutorisationsNummer,
     string AutorisationsType,
     List<Guid> KlinikIds,
     List<Guid> BehandlingstypeIds
     );
}
