using System.ComponentModel.DataAnnotations;

namespace ServicePaymentsAPI.DTOs.Requests
{
    public class RegisterProviderRequestDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Address { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El NIT debe ser mayor que 0")]
        public int Nit { get; set; }

        public int? CellPhone { get; set; }

        [Required]
        [StringLength(20)]
        public string ServiceType { get; set; } = string.Empty;

        [StringLength(50)]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
        public string? Email { get; set; }

        [Required]
        [StringLength(10)]
        public string UserRegistration { get; set; } = string.Empty;
    }
}
