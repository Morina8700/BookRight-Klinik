using BookRight.Domain.Enums;
using BookRight.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Aggregates
{
    public class Kampagne
    {
        public Guid KampagneId { get; private set; }
        public string Navn { get; private set; } = string.Empty;

        public KampagnePeriode Periode { get; private set; }

        public RabatProcent Rabatprocent { get; private set; }

        public List<BehandlingsType> GaeldendeBehandlingstyper { get; private set; }

        public bool Aktiv { get; private set; }

        public Kampagne(
            Guid kampagneId,
            string navn,
            KampagnePeriode periode,
            RabatProcent rabatprocent,
            List<BehandlingsType> gaeldendeBehandlingstyper,
            bool aktiv = true)
        {
            KampagneId = kampagneId;
            Navn = navn;
            Periode = periode;
            Rabatprocent = rabatprocent;
            GaeldendeBehandlingstyper = gaeldendeBehandlingstyper;
            Aktiv = aktiv;
        }
        private Kampagne()
        {
            Periode = null!;
            Rabatprocent = null!;
            GaeldendeBehandlingstyper = new List<BehandlingsType>();
        }
    }
}
