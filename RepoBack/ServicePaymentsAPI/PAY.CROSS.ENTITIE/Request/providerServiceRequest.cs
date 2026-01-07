using System.ComponentModel.DataAnnotations;

namespace PAY.CROSS.ENTITIE.Request
{
    public class providerServiceRequest
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        [Required]
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

        [Required]
        public DateTime DateRegistration { get; set; }

        [Required]
        [StringLength(10)]
        public string? UserModification { get; set; }

        [Required]
        public DateTime DateModification { get; set; }

        [Required]
        public bool State { get; set; }

    }
}
