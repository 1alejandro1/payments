namespace ServicePaymentsAPI.DTOs.Responses
{
    public class RegisterProviderResponseDto : Response
    {
        public Guid ProviderId { get; set; } = Guid.Empty;
    }
}
