namespace ServicePaymentsAPI.DTOs.Responses
{
    public class GetCustomerResponseDto : Response
    {
        public Guid CustomerId { get; set; } = Guid.Empty;

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int IdentificationNumber { get; set; }

        public string IdentificationExtension { get; set; } = string.Empty;

        public string IdentificationComplement { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set; }

        public int? CellPhone { get; set; }

        public string? Email { get; set; }
    }
}
