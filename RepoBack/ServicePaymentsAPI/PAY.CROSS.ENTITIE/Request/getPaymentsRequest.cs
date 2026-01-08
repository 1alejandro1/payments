using System.ComponentModel.DataAnnotations;

namespace PAY.CROSS.ENTITIE.Request
{
    public class getPaymentsRequest
    {
        [Required]
        public Guid PaymentId { get; set; }
    }
}
