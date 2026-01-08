using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PAY.CROSS.ENTITIE.Request;
using PAY.CROSS.ENTITIE.Response;
using PAY.CROSS.LOGGER;

namespace PAY.CROSS.DATAACCESS.Repositorie
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IDataAccess _dataAccess;

        public PaymentRepository(IConfiguration configuration, ILogger logger, IDataAccess dataAccess)
        {
            _configuration = configuration;
            _logger = logger;
            _dataAccess = dataAccess;
        }

        public async Task<paymentsResponse> RegisterPaymentAsync(paymentsRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
        {
            new("@CustomerId", request.CustomerId),
            new("@ProviderId", request.ProviderId),
            new("@Amount", request.Amount),
            new("@CurrencyType", request.CurrencyType),
            new("@Status", request.Status),
            new("@UserRegistration", request.UserRegistration)
        };

              var guidPayment = await _dataAccess.ExecuteSP_string("payments.sp_RegisterPayment", parameters);
                
               return new paymentsResponse { PaymentGuid = guidPayment };
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al registrar payment: {ex.Message}");
                return null!;
            }
        }
    }
}




