using BookRight.Domain.Aggregates;
using BookRight.Domain.Enums;
using BookRight.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace BookRight.UseCases.Commands
{
    public class OpretBehandlerHandler
    {
        private readonly IBehandlerRepository _behandlerRepository;
        private readonly IKlinikRepository _klinikRepository;

        public OpretBehandlerHandler(IBehandlerRepository behandlerRepository, IKlinikRepository klinikRepository)
        {
            _behandlerRepository = behandlerRepository;
            _klinikRepository = klinikRepository;
        }

        public async Task<Guid> HandleAsync(OpretBehandlerCommand command)
        {
            //Parse autorisationstype fra string til enum i domainlaget
            var autorisationsType = Enum.Parse<AutorisationsType>(command.AutorisationsType);

            var behandler = new Behandler(
                command.Fornavn,
                command.Efternavn,
                command.Email,
                command.Telefon,
                command.AutorisationsNummer,
                autorisationsType
            );
           
            // Tilknyt klinikker til behandleren
            foreach (var klinikId in command.KlinikIds)
            {
                var klinik = await _klinikRepository.HentEfterIdAsync(klinikId);
                if (klinik != null)
                {
                    behandler.TilknytKlinik(klinik);
                }
            }

            await _behandlerRepository.AddAsync(behandler);
            return behandler.BehandlerId;
        }

    }
}
