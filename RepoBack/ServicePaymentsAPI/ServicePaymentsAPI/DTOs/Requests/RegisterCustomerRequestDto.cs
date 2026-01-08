using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ServicePaymentsAPI.DTOs.Requests
{
    public class RegisterCustomerRequestDto
    {
        [Required]
        [StringLength(60)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El número de identificación debe ser mayor que 0")]
        public int IdentificationNumber { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "La extensión de identificación debe tener 2 caracteres")]
        [RegularExpression("^[A-Za-z0-9]{2}$", ErrorMessage = "La extensión solo puede contener letras o dígitos")]
        public string IdentificationExtension { get; set; } = string.Empty;

        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El complemento de identificación debe tener 2 caracteres")]
        [RegularExpression("^[A-Za-z0-9]{2}$", ErrorMessage = "El complemento solo puede contener letras o dígitos")]
        public string IdentificationComplement { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public int? CellPhone { get; set; }

        [StringLength(50)]
        [ValidEmail(ErrorMessage = "El correo electrónico no tiene un formato válido")]
        public string? Email { get; set; }

        [Required]
        [StringLength(10)]
        public string UserRegistration { get; set; } = string.Empty;
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ValidEmailAttribute : ValidationAttribute
    {
        private const int DefaultMaxLength = 50;
        private static readonly Regex StructureRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }

            var email = value as string;
            if (string.IsNullOrWhiteSpace(email))
            {
                return true;
            }

            if (email.Length > DefaultMaxLength)
            {
                return false;
            }

            if (email.Contains("..", StringComparison.Ordinal))
            {
                return false;
            }

            if (!StructureRegex.IsMatch(email))
            {
                return false;
            }

            try
            {
                _ = new MailAddress(email);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name) =>
            ErrorMessage ?? "El correo electrónico no tiene un formato válido";
    }
}
