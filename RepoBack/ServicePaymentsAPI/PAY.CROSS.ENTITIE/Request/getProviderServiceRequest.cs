using System.ComponentModel.DataAnnotations;

namespace PAY.CROSS.ENTITIE.Request
{
    public class getProviderServiceRequest
    {
        [Required]
        public Guid ProviderId { get; set; }
    }
}
