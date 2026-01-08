using System.ComponentModel.DataAnnotations;

namespace PAY.CROSS.ENTITIE.Request
{
    public class paymentsRequest
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid ProviderId { get; set; }

        [Required]
        [Range(0.01, 1500, ErrorMessage = "El monto debe ser mayor a 0 y no superar 1500")]
        public decimal Amount { get; set; }


        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El código de moneda debe tener 3 caracteres BOL - USD")]
        public string? CurrencyType { get; set; }

        [Required]
        [StringLength(20)]
        public string? Status { get; set; }

        [Required]
        [StringLength(10)]
        public string? UserRegistration { get; set; }
    }
}
