using System;
using System.Collections.Generic;
using System.Text;
using BookRight.Domain.Enums;

namespace BookRight.Domain.Aggregates
{
   public class Kunde
    {
        public Guid KundeId { get; private set; }
        public string Fornavn { get; private set; } = string.Empty;
        public string Efternavn { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        public string Telefon { get; private set; } = string.Empty;

        public DateOnly Fødselsdato { get; private set; }

        public string Adresse { get; private set; } = string.Empty;

        public string Helbredsnotater { get; private set; } = string.Empty;

        public Guid? ForetrukkenBehandlerID { get; private set; }

        public LoyalitetsNiveau loyalitetsNiveau { get; private set; }


        private Kunde() { } // For EF Core

       

        public Kunde( string fornavn, string efternavn, string email, string telefon, DateOnly fødselsdato, string adresse, string helbredsnotater, Guid? foretrukkenBehandlerID = null)
        {
            if (string.IsNullOrWhiteSpace(fornavn))
                throw new ArgumentException("Fornavn må ikke være tom.");

            if (string.IsNullOrWhiteSpace(efternavn))
                throw new ArgumentException("Efternavn må ikke være tom.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email må ikke være tom.");

            if (string.IsNullOrWhiteSpace(telefon))
                throw new ArgumentException("Telefon må ikke være tom.");

            if (string.IsNullOrWhiteSpace(adresse))
                throw new ArgumentException("Adresse må ikke være tom.");

            KundeId = Guid.NewGuid();
            Fornavn = fornavn;
            Efternavn = efternavn;
            Email = email;
            Telefon = telefon;
            Fødselsdato = fødselsdato;
            Adresse = adresse;
            Helbredsnotater = helbredsnotater;
            ForetrukkenBehandlerID = foretrukkenBehandlerID;
            loyalitetsNiveau = LoyalitetsNiveau.Ingen;
        }

    }

}
