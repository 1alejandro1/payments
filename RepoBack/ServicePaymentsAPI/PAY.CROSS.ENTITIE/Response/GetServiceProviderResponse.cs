namespace PAY.CROSS.ENTITIE.Response
{
    public class GetServiceProviderResponse
    {
        public Guid ProviderId { get; set; } = Guid.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; }

        public int Nit { get; set; }

        public int? CellPhone { get; set; }

        public string ServiceType { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string UserRegistration { get; set; } = string.Empty;

        public DateTime DateRegistration { get; set; }

        public string UserModification { get; set; } = string.Empty;

        public DateTime DateModification { get; set; }

        public bool State { get; set; }
    }
}
