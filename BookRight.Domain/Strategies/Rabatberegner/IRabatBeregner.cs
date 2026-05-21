namespace BookRight.Domain.Strategies.Rabatberegner
{
    // Alle rabatstrategier får samme context og samme resultatobjekt.
    // Strategien returnerer ikke selv resultatet, men forsøger at opdatere det fælles resultat.
    public interface IRabatBeregner
    {
        void BeregnRabat(RabatBeregningContext context, RabatResultat resultat);
    }
}
