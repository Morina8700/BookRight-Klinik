using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.UseCases.Commands
{

    public record OpretKundeCommand(
        string Fornavn,
        string Efternavn,
        string Email,
        string Telefon,
        DateOnly Fødselsdato,
        string Adresse,
        string Helbredsnotater,
        Guid ForetrukkenBehandlerID
    );
}
