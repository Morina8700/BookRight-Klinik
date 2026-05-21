using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
namespace BookRight.Domain.Strategies.Rabatberegner
{
    /*
    public record RabatResultat(
        RabatType RabatType, // Angiver hvilken type rabat der er beregnet
        RabatProcent RabatProcent, // Angiver hvor stor rabatten er i procent, som kan bruges til at vise rabatten til kunden
        Penge PrisUdenRabat, // Pris uden rabat, som kan bruges til at vise den oprindelige pris til kunden
        Penge PrisMedRabat // Pris med rabat, som kan bruges til at vise den endelige pris til kunden
        );
    */

    public sealed class RabatResultat
    {
        private readonly Lock _rabatLock = new Lock();

        public RabatType RabatType {  get; private set; }
        public RabatProcent RabatProcent { get; private set; }

        public Penge PrisUdenRabat {  get; }

        public Penge PrisMedRabat { get; private set; }

        public RabatResultat(Penge prisUdenRabat)
        {
            RabatType = RabatType.Ingen;
            RabatProcent = new RabatProcent(0);
            PrisUdenRabat  = prisUdenRabat;
            PrisMedRabat = prisUdenRabat;
        }

        // Flere tråde kan kalde denne metode samtidig.
        // Lock sikrer, at kun én tråd ad gangen kan sammenligne og opdatere bedste rabat.
        public void OpdaterHvisBedre(
            RabatType rabatType,
            RabatProcent rabatProcent,
            Penge prisMedRabat)
        {
            lock (_rabatLock)
            {
                if(prisMedRabat.Belob < PrisMedRabat.Belob)
                {
                    RabatType = rabatType;
                    RabatProcent = rabatProcent;
                    PrisMedRabat = prisMedRabat;
                }
            }
        }

    }
    

   



    
    
    

}
