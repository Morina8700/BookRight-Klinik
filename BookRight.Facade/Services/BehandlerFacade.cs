using BookRight.Facade.Contracts.Behandler;
using BookRight.Facade.Contracts.Kunder;
using BookRight.Facade.Interfaces;
using BookRight.UseCases.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using BookRight.Facade.Contracts.Bookinger;

namespace BookRight.Facade.Services
{
    public class BehandlerFacade : IBehandlerFacade
    {
        private readonly OpretBehandlerHandler _opretBehandlerHandler;
        public BehandlerFacade(OpretBehandlerHandler opretBehandlerHandler)
        {
            _opretBehandlerHandler = opretBehandlerHandler;
        }

        public async Task<Guid> OpretBehandlerAsync(OpretBehandlerRequest request)
        {
            var command = new OpretBehandlerCommand(
            Guid.NewGuid(),
            request.Fornavn,
            request.Efternavn,
            request.Email,
            request.Telefon,
            request.AutorisationsNummer,
            request.AutorisationsType.ToString(),
            request.KlinikIds
            );

            return await _opretBehandlerHandler.HandleAsync(command);
        }


    }
}
