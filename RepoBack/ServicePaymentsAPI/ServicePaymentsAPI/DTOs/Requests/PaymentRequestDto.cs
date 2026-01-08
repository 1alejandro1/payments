using System.ComponentModel.DataAnnotations;

namespace ServicePaymentsAPI.DTOs.Requests
{
    public class PaymentRequestDto
    {
        [Required]
        [Range(0.01, 1500, ErrorMessage = "El monto debe ser mayor a 0 y no superar 1500")]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El código de moneda debe tener 3 caracteres BOL - USD")]
        [RegularExpression("^BOL$", ErrorMessage = "Solo se permite la moneda Bolivianos (BOL)")]
        public string Currency { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string Status { get; set; } = string.Empty;
        [Required]
        [StringLength(10)]
        public string UserRegistration { get; set; } = string.Empty;
        [Required]
        public Guid CustomerId { get; set; } = Guid.Empty;
        [Required]
        public Guid ProviderId { get; set; } = Guid.Empty;
    }
}
