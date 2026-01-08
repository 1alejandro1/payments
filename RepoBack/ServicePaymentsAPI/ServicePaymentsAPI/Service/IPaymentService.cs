using PAY.CROSS.ENTITIE.Request;
using ServicePaymentsAPI.DTOs.Requests;
using ServicePaymentsAPI.DTOs.Responses;

namespace ServicePaymentsAPI
{
    public interface IPaymentService
    {
        Task<RegisterPaymentResponseDto> RegisterPaymentAsync(RegisterPaymentRequestDto request);
        Task<RegisterCustomerResponseDto> RegisterCustomerAsync(RegisterCustomerRequestDto request);
        Task<RegisterProviderResponseDto> RegisterServiceProviderAsync(RegisterProviderRequestDto request);
        Task<List<GetPaymentResponseDto>> GetPaymentAsync(GetPaymentRequestDto request);
        Task<List<GetCustomerResponseDto>> GetCustomerAsync(GetCustomerRequestDto request);
        Task<List<GetProviderResponseDto>> GetServiceProviderAsync(GetProviderRequestDto request);
    }
}
