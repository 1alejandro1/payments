using PAY.CROSS.ENTITIE.Response;
using PAY.CROSS.ENTITIE.Request;

namespace PAY.CROSS.DATAACCESS.Repositorie
{
    public interface IPaymentRepository
    {
        Task<RegisterPaymentsResponse> RegisterPaymentAsync(registerPaymentsRequest request);
        Task<RegisterCustomerResponse> RegisterCustomerAsync(registerCustomerRequest request);
        Task<RegisterServiceProviderResponse> RegisterServiceProviderAsync(registerProviderServiceRequest request);
        Task<List<GetPaymentsResponse>> GetPaymentAsync(getPaymentsRequest request);
        Task<List<GetCustomerResponse>> GetCustomerAsync(getCustomerRequest request);
        Task<List<GetServiceProviderResponse>> GetServiceProviderAsync(getProviderServiceRequest request);
    }
}
