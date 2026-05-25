using BookRight.Domain.Interfaces;
using DomainKunde = BookRight.Domain.Aggregates.Kunde;

namespace BookRight.UseCases.Commands.Kunde
{
    public class OpretKundeHandler
    {
        private readonly IKundeRepository _kundeRepository;

        public OpretKundeHandler(IKundeRepository kundeRepository)
        {
            _kundeRepository = kundeRepository;
        }

        public async Task<Guid> HandleAsync(OpretKundeCommand command)
        {
            if (await _kundeRepository.EmailFindesAsync(command.Email))
            {
                throw new InvalidOperationException("Der findes allerede en kunde med den email.");
            }

            if (await _kundeRepository.TelefonFindesAsync(command.Telefon))
            {
                throw new InvalidOperationException("Der findes allerede en kunde med det telefonnummer.");
            }

            var kunde = new DomainKunde(
                command.Fornavn,
                command.Efternavn,
                command.Email,
                command.Telefon,
                command.Fødselsdato,
                command.Adresse,
                command.Helbredsnotater,
                command.ForetrukkenBehandlerID
            );
            await _kundeRepository.TilføjAsync(kunde);
            return kunde.KundeId;
        }
    }
}
