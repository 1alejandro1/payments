using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PAY.CROSS.DATAACCESS.Repositorie;
using PAY.CROSS.ENTITIE.Request;
using PAY.CROSS.ENTITIE.Response;
using ServicePaymentsAPI.DTOs.Requests;
using ServicePaymentsAPI.DTOs.Responses;
using System.Data;

namespace ServicePaymentsAPI
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IConfiguration configuration, IPaymentRepository paymentRepository)
        {
            _configuration = configuration;
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentResponseDto> RegisterPaymentAsync(PaymentRequestDto request)
        {
            var paymentRequest = new paymentsRequest
            {
                CustomerId = request.CustomerId,
                ProviderId = request.ProviderId,
                Amount = request.Amount,
                CurrencyType = request.Currency,
                Status = request.Status,
                UserRegistration = request.UserRegistration
            };

            var paymentResult = await _paymentRepository.RegisterPaymentAsync(paymentRequest);

            var response = new PaymentResponseDto
            {
                Success = paymentResult != null,
                Message = paymentResult != null ? "Payment registered successfully." : "Payment registration failed."
            };

            if (paymentResult != null)
            {
                response.PaymentId = Guid.TryParse(paymentResult.PaymentGuid, out var paymentGuid) ? paymentGuid : null;
                if (response.PaymentId == null)
                {
                    response.Success = false;
                    response.Message = "Payment registered, but PaymentGuid is invalid.";
                }
            }

            return response;
        }
    }
}
