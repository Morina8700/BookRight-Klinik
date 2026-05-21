namespace BookRight.Domain.Strategies.Rabatberegner
{
    public class RabatBeregnerService
    {
        private readonly IReadOnlyCollection<IRabatBeregner> _rabatBeregnere;

        public RabatBeregnerService(IEnumerable<IRabatBeregner> rabatBeregnere)
        {
            _rabatBeregnere = rabatBeregnere.ToList();
        }

        public async Task<RabatResultat> BeregnBedsteRabatAsync(RabatBeregningContext context)
        {
            // Det fælles resultat starter med ingen rabat og bliver opdateret af strategierne.
            var resultat = new RabatResultat(context.PrisUdenRabat);

            // Rabatberegning er CPU-bound arbejde, så hver strategi køres parallelt på thread poolen.
            var tasks =_rabatBeregnere.Select(beregner =>
            Task.Run(() => beregner.BeregnRabat(context, resultat)));

            // Her venter vi på, at loyalitet, fødselsdag og kampagne alle er færdige.
            await Task.WhenAll(tasks);

            return resultat;

        }



}
}
