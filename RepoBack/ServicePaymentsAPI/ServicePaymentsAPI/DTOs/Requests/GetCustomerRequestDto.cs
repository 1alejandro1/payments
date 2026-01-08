using System.ComponentModel.DataAnnotations;

namespace ServicePaymentsAPI.DTOs.Requests
{
    public class GetCustomerRequestDto
    {
        [Required]
        public Guid CustomerId { get; set; }
    }    
}
