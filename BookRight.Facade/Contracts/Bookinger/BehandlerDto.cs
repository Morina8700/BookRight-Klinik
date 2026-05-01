namespace BookRight.Facade.Contracts.Bookinger
{
    public class BehandlerDto
    {
        public Guid BehandlerId { get; set; }
        public string FuldeNavn { get; set; } = string.Empty;
        public string AutorisationsType { get; set; } = string.Empty;
        public IEnumerable<KlinikDto> Klinikker { get; set; } = new List<KlinikDto>();
        public IEnumerable<BehandlingstypeDto> Behandlingstyper { get; set; } = new List<BehandlingstypeDto>();
    }
}