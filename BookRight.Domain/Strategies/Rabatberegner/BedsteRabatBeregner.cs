using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
using System.Collections.Concurrent;

namespace BookRight.Domain.Strategies.Rabatberegner
{
    public class BedsteRabatBeregner
    {
        private readonly IReadOnlyCollection<IRabatBeregner> _rabatBeregnere;

        public BedsteRabatBeregner(IEnumerable<IRabatBeregner> rabatBeregnere)
        {
            _rabatBeregnere = rabatBeregnere.ToList();
        }

        public RabatResultat BeregnBedsteRabat(RabatBeregningContext context)
        {
            if(_rabatBeregnere.Count == 0)
            {
                return new RabatResultat(
                    RabatType.Ingen,
                    new RabatProcent(0),
                    context.PrisUdenRabat,
                    context.PrisUdenRabat
                );
            }

            //Her bruges CPU-bound parallelisme. Hver rabatstrategi kører parallelt på thread poolen. Resultaterne samles i en ConcurrentBag,
            //Som er thread-safe og derfor beskytter mod race conditions.
            var resultater = new ConcurrentBag<RabatResultat>();

            Parallel.ForEach(_rabatBeregnere, beregner =>
            {
                var resultat = beregner.BeregnRabat(context);
                resultater.Add(resultat);
            });

            // Når alle strategier er færdige, sorteres resultaterne efter laveste pris. Den billigste pris vælges automatisk.
            return resultater
                .OrderBy(r => r.PrisMedRabat.Belob)
                .ThenByDescending(r => r.RabatProcent.Value)
                .First();
        }
    }
}
