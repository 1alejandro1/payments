using System.ComponentModel.DataAnnotations;

namespace ServicePaymentsAPI.DTOs.Requests
{
    public class GetProviderRequestDto
    {
        [Required]
        public Guid ProviderId { get; set; }
    }
}
