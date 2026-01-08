using System.ComponentModel.DataAnnotations;

namespace ServicePaymentsAPI.DTOs.Requests
{
    public class GetPaymentRequestDto
    {        
        [Required]
        public Guid PaymentId { get; set; } = Guid.Empty;
    }
}
