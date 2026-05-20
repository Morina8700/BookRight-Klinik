using BookRight.Domain.Aggregates;
using BookRight.Domain.Interfaces;

namespace BookRight.UseCases.Commands
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
            var kunde = new Kunde(
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
