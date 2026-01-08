namespace ServicePaymentsAPI.DTOs.Responses
{
    public class GetProviderResponseDto : Response
    {
        public Guid ProviderId { get; set; } = Guid.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; }

        public int Nit { get; set; }

        public int? CellPhone { get; set; }

        public string ServiceType { get; set; } = string.Empty;

        public string? Email { get; set; }
    }
}
