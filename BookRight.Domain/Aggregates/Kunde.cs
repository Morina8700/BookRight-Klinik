using System;
using System.Collections.Generic;
using System.Text;
using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Aggregates
{
   public class Kunde
    {
        public Guid KundeId { get; private set; }
        public string Fornavn { get; private set; } = string.Empty;
        public string Efternavn { get; private set; } = string.Empty;

        public Email Email { get; private set; } = null;

        public Telefon Telefon { get; private set; } = null;

        public DateOnly Fødselsdato { get; private set; }

        public string Adresse { get; private set; } = string.Empty;

        public string Helbredsnotater { get; private set; } = string.Empty;

        public Guid? ForetrukkenBehandlerID { get; private set; }

        public LoyalitetsNiveau loyalitetsNiveau { get; private set; }

        public bool FoedselsdagsrabatBrugt { get; private set; }


        private Kunde() { } // For EF Core

       

        public Kunde( string fornavn, string efternavn, string email, string telefon, DateOnly fødselsdato, string adresse, string helbredsnotater, Guid? foretrukkenBehandlerID = null)
        {

            if (string.IsNullOrWhiteSpace(fornavn) && string.IsNullOrWhiteSpace(efternavn) && string.IsNullOrWhiteSpace(email) 
                && string.IsNullOrWhiteSpace(telefon) && string.IsNullOrWhiteSpace(adresse) && string.IsNullOrWhiteSpace(helbredsnotater))
            {
                throw new ArgumentException("Du skal udfylde kundeoplysninger før kunden kan oprettes.");
            }

            if (string.IsNullOrWhiteSpace(fornavn))
                throw new ArgumentException("Fornavn må ikke være tom.");

            if (string.IsNullOrWhiteSpace(efternavn))
                throw new ArgumentException("Efternavn må ikke være tom.");

            if (string.IsNullOrWhiteSpace(adresse))
                throw new ArgumentException("Adresse må ikke være tom.");




            KundeId = Guid.NewGuid();
            Fornavn = fornavn;
            Efternavn = efternavn;
            Email = new Email(email);
            Telefon = new Telefon(telefon);
            Fødselsdato = fødselsdato;
            Adresse = adresse;
            Helbredsnotater = helbredsnotater;
            ForetrukkenBehandlerID = foretrukkenBehandlerID;
            loyalitetsNiveau = LoyalitetsNiveau.Ingen;
            FoedselsdagsrabatBrugt = false;
        }

        public void MarkerFoedselsdagsrabatBrugt()
        {
            FoedselsdagsrabatBrugt = true;
        }

    }

}
