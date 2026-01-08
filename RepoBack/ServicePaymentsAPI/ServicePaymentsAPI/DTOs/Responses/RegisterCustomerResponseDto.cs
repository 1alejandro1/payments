namespace ServicePaymentsAPI.DTOs.Responses
{
    public class RegisterCustomerResponseDto : Response
    {
        public Guid CustomerId { get; set; } = Guid.Empty;

    }
}
