using BookRight.Domain.Enums;
namespace BookRight.Domain.Strategies.Rabatberegner
{
    public class KampagneRabatBeregner : IRabatBeregner
    {
        // Kampagnestrategien finder den bedste aktive kampagne, der matcher dato og behandlingstype.
        public void BeregnRabat(RabatBeregningContext context, RabatResultat resultat)
        {
           var kampagne = context.AktivKampagner
                .Where(k => k.Aktiv) // Sikrer at kampagnen er markeret som aktiv
                .Where(k => k.Periode.Indeholder(context.BookingDato)) // Sikrer at kampagnen er aktiv på bookingdatoen
                .Where(k=> k.GaeldendeBehandlingstyper.Any(type =>
                context.Behandlingstyper.Contains(type))) // Sikrer at kampagnen gælder for mindst en af de behandlingstyper, der er i konteksten
                .OrderByDescending(k => k.Rabatprocent.Value) // Hvis der er flere kampagner, vælger vi den med den højeste rabatprocent
                .FirstOrDefault();

            if (kampagne is null) // Hvis der ikke er nogen kampagne, der opfylder kriterierne, returnerer vi et resultat med ingen rabat
            {
                return;
            }

            var prisMedRabat = context.PrisUdenRabat.FratraekRabat(kampagne.Rabatprocent); // Beregner prisen efter rabat ved at trække kampagnens rabatprocent fra den oprindelige pris

            resultat.OpdaterHvisBedre(
                RabatType.Kampagne,
                kampagne.Rabatprocent,
                prisMedRabat

                );
        }
    }
}
