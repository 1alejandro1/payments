namespace ServicePaymentsAPI.DTOs.Responses
{
    public class GetPaymentResponseDto : Response
    {
        public Guid PaymentId { get; set; } = Guid.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string ProviderName { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Currency { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string UserRegistration { get; set; } = string.Empty;

        public DateTime DateRegistration { get; set; }

        public string UserModification { get; set; } = string.Empty;

        public DateTime DateModification { get; set; }

        public bool State { get; set; }
    }
}
