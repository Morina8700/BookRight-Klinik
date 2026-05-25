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
        private readonly IBehandlingstypeRepository _behandlingstypeRepository;

        public OpretBehandlerHandler(
            IBehandlerRepository behandlerRepository,
            IKlinikRepository klinikRepository,
            IBehandlingstypeRepository behandlingstypeRepository)
        {
            _behandlerRepository = behandlerRepository;
            _klinikRepository = klinikRepository;
            _behandlingstypeRepository = behandlingstypeRepository;
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

            foreach (var behandlingstypeId in command.BehandlingstypeIds)
            {
                var behandlingstype = await _behandlingstypeRepository.HentEfterIdAsync(behandlingstypeId)
                    ?? throw new InvalidOperationException("Behandlingstype ikke fundet");

                behandler.TilknytBehandlingstype(behandlingstype);
            }

            await _behandlerRepository.AddAsync(behandler);
            return behandler.BehandlerId;
        }

    }
}
