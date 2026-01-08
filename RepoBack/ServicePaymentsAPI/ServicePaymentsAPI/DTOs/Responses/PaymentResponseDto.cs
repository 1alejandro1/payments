namespace ServicePaymentsAPI.DTOs.Responses
{
    public class PaymentResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid? PaymentId { get; set; }
    }
}
