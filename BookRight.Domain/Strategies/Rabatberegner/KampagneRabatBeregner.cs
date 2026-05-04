using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Strategies.Rabatberegner
{
    public class KampagneRabatBeregner : IRabatBeregner
    {
        public RabatResultat BeregnRabat(RabatBeregningContext context)
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
                var ingenRabat = new RabatProcent(0);
                return new RabatResultat(
                    RabatType.Ingen,
                    ingenRabat,
                    context.PrisUdenRabat,
                    context.PrisUdenRabat
                    );
                


                
            }

            var prisMedRabat = context.PrisUdenRabat.FratraekRabat(kampagne.Rabatprocent); // Beregner prisen efter rabat ved at trække kampagnens rabatprocent fra den oprindelige pris
            return new RabatResultat(
                RabatType.Kampagne,
                kampagne.Rabatprocent,
                context.PrisUdenRabat,
                prisMedRabat
                );
        }
    }
}
