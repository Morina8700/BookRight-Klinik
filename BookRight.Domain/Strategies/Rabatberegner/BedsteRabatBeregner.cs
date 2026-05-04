using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Strategies.Rabatberegner
{
    public class BedsteRabatBeregner
    {
        private readonly IReadOnlyCollection<IRabatBeregner> _rabatBeregnere;

        public BedsteRabatBeregner(IEnumerable<IRabatBeregner> rabatBeregnere)
        {
            _rabatBeregnere = rabatBeregnere.ToList();
        }

        public async Task<RabatResultat> BeregnBedsteRabatAsync(RabatBeregningContext context)
        {
            if (_rabatBeregnere.Count == 0)
            {
                return new RabatResultat(
                    RabatType.Ingen,
                    new RabatProcent(0),
                    context.PrisUdenRabat,
                    context.PrisUdenRabat);
            }

            var beregninger = _rabatBeregnere
                .Select(beregner => Task.Run(() => beregner.BeregnRabat(context)));

            var resultater = await Task.WhenAll(beregninger);

            return resultater
                .OrderBy(resultat => resultat.PrisMedRabat.Belob)
                .ThenByDescending(resultat => resultat.RabatProcent.Value)
                .First();
        }
    }
}
