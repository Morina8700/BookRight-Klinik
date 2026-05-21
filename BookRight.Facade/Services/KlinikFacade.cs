using BookRight.Domain.Interfaces;
using BookRight.Facade.Contracts.Bookinger;
using BookRight.Facade.Interfaces;

namespace BookRight.Facade
{
    public class KlinikFacade : IKlinikFacade
    {
        private readonly IKlinikRepository _klinikRepository;

        public KlinikFacade(IKlinikRepository klinikRepository)
        {
            _klinikRepository = klinikRepository;
        }

        public async Task<IEnumerable<KlinikDto>> HentAlleAsync()
        {
            var klinikker = await _klinikRepository.HentAlleAsync();
            return klinikker.Select(k => new KlinikDto
            {
                KlinikId = k.KlinikId,
                Navn = k.Navn ?? "",
                Adresse = k.Adresse ?? "",
                AntalRum = k.AntalRum
            });
        }
    }
}
