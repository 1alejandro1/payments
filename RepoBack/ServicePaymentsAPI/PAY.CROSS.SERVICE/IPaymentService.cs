using PAY.CROSS.ENTITIE.Request;
using ServicePaymentsAPI.DTOs.Requests;
using ServicePaymentsAPI.DTOs.Responses;

namespace PAY.CROSS.SERVICE
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> RegisterPaymentAsync(PaymentRequestDto request);
    }
}
