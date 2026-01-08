using PAY.CROSS.ENTITIE.Response;
using PAY.CROSS.ENTITIE.Request;

namespace PAY.CROSS.DATAACCESS.Repositorie
{
    public interface IPaymentRepository
    {
        Task<paymentsResponse> RegisterPaymentAsync(paymentsRequest request);
    }
}
