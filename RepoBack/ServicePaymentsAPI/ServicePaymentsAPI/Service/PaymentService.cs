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

        public async Task<RegisterPaymentResponseDto> RegisterPaymentAsync(RegisterPaymentRequestDto request)
        {
            var paymentRequest = new registerPaymentsRequest
            {
                CustomerId = request.CustomerId,
                ProviderId = request.ProviderId,
                Amount = request.Amount,
                CurrencyType = request.Currency,
                Status = request.Status,
                UserRegistration = request.UserRegistration
            };

            var paymentResult = await _paymentRepository.RegisterPaymentAsync(paymentRequest);

            var response = new RegisterPaymentResponseDto
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
        public async Task<RegisterCustomerResponseDto> RegisterCustomerAsync(RegisterCustomerRequestDto request)
        {
            var repoRequest = new registerCustomerRequest
            {
                Name = request.Name,
                LastName = request.LastName,
                IdentificationNumber = request.IdentificationNumber,
                IdentificationExtension = request.IdentificationExtension,
                IdentificationComplement = request.IdentificationComplement,
                BirthDate = request.BirthDate,
                CellPhone = request.CellPhone,
                Email = request.Email,
                UserRegistration = request.UserRegistration
            };

            var repoResult = await _paymentRepository.RegisterCustomerAsync(repoRequest);

            var response = new RegisterCustomerResponseDto
            {
                Success = repoResult != null,
                Message = repoResult != null ? "Customer registered successfully." : "Customer registration failed."
            };

            if (repoResult != null)
            {
                response.CustomerId = Guid.TryParse(repoResult.CustomerGuid, out var guid) ? guid : Guid.Empty;
                if (response.CustomerId == Guid.Empty)
                {
                    response.Success = false;
                    response.Message = "Customer registered, but CustomerGuid is invalid.";
                }
            }

            return response;
        }
        public async Task<RegisterProviderResponseDto> RegisterServiceProviderAsync(RegisterProviderRequestDto request)
        {
            var repoRequest = new registerProviderServiceRequest
            {
                Name = request.Name,
                Address = request.Address,
                Nit = request.Nit,
                CellPhone = request.CellPhone,
                ServiceType = request.ServiceType,
                Email = request.Email,
                UserRegistration = request.UserRegistration
            };

            var repoResult = await _paymentRepository.RegisterServiceProviderAsync(repoRequest);

            var response = new RegisterProviderResponseDto
            {
                Success = repoResult != null,
                Message = repoResult != null ? "Service provider registered successfully." : "Service provider registration failed."
            };

            if (repoResult != null)
            {
                response.ProviderId = Guid.TryParse(repoResult.ServiceProviderGuid, out var guid) ? guid : Guid.Empty;
                if (response.ProviderId == Guid.Empty)
                {
                    response.Success = false;
                    response.Message = "Service provider registered, but ServiceProviderGuid is invalid.";
                }
            }

            return response;
        }
        public async Task<List<GetPaymentResponseDto>> GetPaymentAsync(GetPaymentRequestDto request)
        {
            var repoRequest = new getPaymentsRequest
            {
                PaymentId = request.PaymentId
            };

            var repoResult = await _paymentRepository.GetPaymentAsync(repoRequest);

            if (repoResult == null || repoResult.Count == 0)
                return new List<GetPaymentResponseDto>();

            var list = repoResult.Select(r => new GetPaymentResponseDto
            {
                PaymentId = r.PaymentId,
                CustomerName = r.CustomerName,
                ProviderName = r.ProviderName,
                Amount = r.Amount,
                Currency = r.CurrencyType,
                Status = r.Status,
                UserRegistration = r.UserRegistration,
                DateRegistration = r.DateRegistration,
                UserModification = r.UserModification,
                DateModification = r.DateModification,
                State = r.State,
                Success = true,
                Message = "OK"
            }).ToList();

            return list;
        }
        public async Task<List<GetCustomerResponseDto>> GetCustomerAsync(GetCustomerRequestDto request)
        {
            var repoRequest = new getCustomerRequest
            {
                CustomerId = request.CustomerId
            };

            var repoResult = await _paymentRepository.GetCustomerAsync(repoRequest);

            if (repoResult == null || repoResult.Count == 0)
                return new List<GetCustomerResponseDto>();

            var list = repoResult.Select(r => new GetCustomerResponseDto
            {
                CustomerId = r.CustomerId,
                Name = r.Name,
                LastName = r.LastName,
                IdentificationNumber = r.IdentificationNumber,
                IdentificationExtension = r.IdentificationExtension,
                IdentificationComplement = r.IdentificationComplement,
                BirthDate = r.BirthDate,
                CellPhone = r.CellPhone,
                Email = r.Email,
                Success = true,
                Message = "OK"
            }).ToList();

            return list;
        }

        public async Task<List<GetProviderResponseDto>> GetServiceProviderAsync(GetProviderRequestDto request)
        {
            var repoRequest = new getProviderServiceRequest
            {
                ProviderId = request.ProviderId
            };

            var repoResult = await _paymentRepository.GetServiceProviderAsync(repoRequest);

            if (repoResult == null || repoResult.Count == 0)
                return new List<GetProviderResponseDto>();

            var list = repoResult.Select(r => new GetProviderResponseDto
            {
                ProviderId = r.ProviderId,
                Name = r.Name,
                Address = r.Address,
                Nit = r.Nit,
                CellPhone = r.CellPhone,
                ServiceType = r.ServiceType,
                Email = r.Email,
                Success = true,
                Message = "OK"
            }).ToList();

            return list;
        }
    }
}
