using System.ComponentModel.DataAnnotations;

namespace PAY.CROSS.ENTITIE.Request
{
    public class registerCustomerRequest
    {
        [Required]
        [StringLength(60)]
        public string? Name { get; set; }

        [Required]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Required]
        public int IdentificationNumber { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string? IdentificationExtension { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string? IdentificationComplement { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public int? CellPhone { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string? Email { get; set; }

        [Required]
        [StringLength(10)]
        public string? UserRegistration { get; set; }
    }
}
