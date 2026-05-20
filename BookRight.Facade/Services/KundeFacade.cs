using System;
using System.Collections.Generic;
using System.Text;
using BookRight.Facade.Interfaces;
using BookRight.Facade.Contracts.Kunder;
using BookRight.UseCases.Commands.Kunde;


namespace BookRight.Facade.Services
{
    public class KundeFacade : IKundeFacade
    {
        private readonly OpretKundeHandler _opretKundeHandler;

        public KundeFacade(OpretKundeHandler opretKundeHandler)
        {
            _opretKundeHandler = opretKundeHandler;
        }

        public async Task<Guid> OpretKundeAsync(OpretKundeRequest request)
        {
            var command = new OpretKundeCommand(
                request.Fornavn,
                request.Efternavn,
                request.Email,
                request.Telefon,
                request.Fødselsdato,
                request.Adresse,
                request.Helbredsnotater,
                request.ForetrukkenBehandlerId
            );

            return await _opretKundeHandler.HandleAsync(command);
        }
    }
}
