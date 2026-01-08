using System.ComponentModel.DataAnnotations;

namespace PAY.CROSS.ENTITIE.Request
{
    public class getCustomerRequest
    {
        [Required]
        public Guid CustomerId { get; set; }
    }
}
