using System.ComponentModel.DataAnnotations;

namespace PAY.CROSS.ENTITIE.Request
{
    public class registerProviderServiceRequest
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El NIT debe ser mayor que 0")]
        public int Nit { get; set; }

        public int? CellPhone { get; set; }

        [Required]
        [StringLength(20)]
        public string? ServiceType { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string? Email { get; set; }

        [Required]
        [StringLength(10)]
        public string? UserRegistration { get; set; }

    }
}
