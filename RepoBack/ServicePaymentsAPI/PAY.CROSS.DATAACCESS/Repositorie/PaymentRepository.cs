using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PAY.CROSS.ENTITIE.Request;
using PAY.CROSS.ENTITIE.Response;
using PAY.CROSS.LOGGER;
using System.Data;

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

        public async Task<RegisterPaymentsResponse> RegisterPaymentAsync(registerPaymentsRequest request)
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

                return new RegisterPaymentsResponse { PaymentGuid = guidPayment };
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al registrar payment: {ex.Message}");
                return null!;
            }
        }
        public async Task<RegisterCustomerResponse> RegisterCustomerAsync(registerCustomerRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
        {
            new("@Name", request.Name ?? (object)DBNull.Value),
            new("@LastName", request.LastName ?? (object)DBNull.Value),
            new("@IdentificationNumber", request.IdentificationNumber),
            new("@IdentificationExtension", request.IdentificationExtension ?? (object)DBNull.Value),
            new("@IdentificationComplement", request.IdentificationComplement ?? (object)DBNull.Value),
            new("@BirthDate", request.BirthDate.HasValue ? (object)request.BirthDate.Value : DBNull.Value),
            new("@CellPhone", request.CellPhone.HasValue ? (object)request.CellPhone.Value : DBNull.Value),
            new("@Email", string.IsNullOrWhiteSpace(request.Email) ? (object)DBNull.Value : request.Email),
            new("@UserRegistration", request.UserRegistration ?? (object)DBNull.Value)
        };

                var customerGuid = await _dataAccess.ExecuteSP_string("payments.sp_RegisterCustomer", parameters);

                return new RegisterCustomerResponse { CustomerGuid = customerGuid };
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al registrar customer: {ex.Message}");
                return null!;
            }
        }
        public async Task<RegisterServiceProviderResponse> RegisterServiceProviderAsync(registerProviderServiceRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
        {
            new("@Name", request.Name ?? (object)DBNull.Value),
            new("@Address", string.IsNullOrWhiteSpace(request.Address) ? (object)DBNull.Value : request.Address),
            new("@Nit", request.Nit),
            new("@CellPhone", request.CellPhone.HasValue ? (object)request.CellPhone.Value : DBNull.Value),
            new("@ServiceType", request.ServiceType ?? (object)DBNull.Value),
            new("@Email", string.IsNullOrWhiteSpace(request.Email) ? (object)DBNull.Value : request.Email),
            new("@UserRegistration", request.UserRegistration ?? (object)DBNull.Value)
        };

                var serviceProviderGuid = await _dataAccess.ExecuteSP_string("payments.sp_RegisterServiceProvider", parameters);

                return new RegisterServiceProviderResponse { ServiceProviderGuid = serviceProviderGuid };
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al registrar service provider: {ex.Message}");
                return null!;
            }
        }
        public async Task<List<GetPaymentsResponse>> GetPaymentAsync(getPaymentsRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new("@PaymentId", request.PaymentId)
                };

                var dt = await _dataAccess.ExecuteSP_DataTable("payments.sp_GetPayments", parameters);

                var result = new List<GetPaymentsResponse>();

                if (dt == null || dt.Rows.Count == 0)
                    return result;

                string GetString(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                            return r[n]?.ToString() ?? string.Empty;
                    }
                    return string.Empty;
                }

                Guid GetGuid(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                        {
                            if (Guid.TryParse(r[n].ToString(), out var g))
                                return g;
                        }
                    }
                    return Guid.Empty;
                }

                decimal GetDecimal(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                        {
                            if (decimal.TryParse(r[n].ToString(), out var d))
                                return d;
                        }
                    }
                    return 0m;
                }

                DateTime GetDateTime(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                        {
                            if (DateTime.TryParse(r[n].ToString(), out var d))
                                return d;
                        }
                    }
                    return DateTime.MinValue;
                }

                bool GetBool(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                        {
                            if (bool.TryParse(r[n].ToString(), out var b))
                                return b;
                            if (int.TryParse(r[n].ToString(), out var i))
                                return i != 0;
                        }
                    }
                    return false;
                }

                foreach (DataRow row in dt.Rows)
                {
                    var item = new GetPaymentsResponse
                    {
                        PaymentId = GetGuid(row, "payment_id_ui", "payment_id", "PaymentId"),
                        CustomerName = GetString(row, "customer_name_vc", "customer_name", "CustomerName", "cm_name_vc", "name_vc_cm", "name_vc"),
                        ProviderName = GetString(row, "provider_name_vc", "provider_name", "ProviderName", "sp_name_vc", "name_vc_provider", "name_vc"),
                        Amount = GetDecimal(row, "amount_dc", "amount", "Amount"),
                        CurrencyType = GetString(row, "currency_type_ch", "currency", "CurrencyType"),
                        Status = GetString(row, "status_vc", "status", "Status"),
                        UserRegistration = GetString(row, "user_registration_vc", "user_registration", "UserRegistration"),
                        DateRegistration = GetDateTime(row, "date_registration_dt", "date_registration", "DateRegistration"),
                        UserModification = GetString(row, "user_modification_vc", "user_modification", "UserModification"),
                        DateModification = GetDateTime(row, "date_modification_dt", "date_modification", "DateModification"),
                        State = GetBool(row, "state_bt", "state", "State")
                    };

                    result.Add(item);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al obtener pagos: {ex.Message}");
                return null!;
            }
        }
        public async Task<List<GetCustomerResponse>> GetCustomerAsync(getCustomerRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new("@CustomerId", request.CustomerId)
                };

                var dt = await _dataAccess.ExecuteSP_DataTable("payments.sp_GetCustomers", parameters);

                var result = new List<GetCustomerResponse>();

                if (dt == null || dt.Rows.Count == 0)
                    return result;

                // helpers locales
                string GetString(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                            return r[n]?.ToString() ?? string.Empty;
                    }
                    return string.Empty;
                }

                Guid GetGuid(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                        {
                            if (Guid.TryParse(r[n].ToString(), out var g))
                                return g;
                        }
                    }
                    return Guid.Empty;
                }

                int GetInt(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                        {
                            if (int.TryParse(r[n].ToString(), out var i))
                                return i;
                        }
                    }
                    return 0;
                }

                DateTime? GetNullableDateTime(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                        {
                            if (DateTime.TryParse(r[n].ToString(), out var d))
                                return d;
                        }
                    }
                    return null;
                }

                bool GetBool(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                        {
                            if (bool.TryParse(r[n].ToString(), out var b))
                                return b;
                            if (int.TryParse(r[n].ToString(), out var i))
                                return i != 0;
                        }
                    }
                    return false;
                }

                foreach (DataRow row in dt.Rows)
                {
                    var item = new GetCustomerResponse
                    {
                        CustomerId = GetGuid(row, "customer_id_ui", "customer_id", "CustomerId"),
                        Name = GetString(row, "name_vc", "name", "Name"),
                        LastName = GetString(row, "last_name_vc", "last_name", "LastName"),
                        IdentificationNumber = GetInt(row, "identification_number_in", "identification_number", "IdentificationNumber"),
                        IdentificationExtension = GetString(row, "identification_extension_ch", "identification_extension", "IdentificationExtension"),
                        IdentificationComplement = GetString(row, "identification_complement_ch", "identification_complement", "IdentificationComplement"),
                        BirthDate = GetNullableDateTime(row, "birth_date_dt", "birth_date", "BirthDate"),
                        CellPhone = GetInt(row, "cell_phone_in", "cell_phone", "CellPhone"),
                        Email = string.IsNullOrWhiteSpace(GetString(row, "email_vc", "email", "Email")) ? null : GetString(row, "email_vc", "email", "Email"),
                        UserRegistration = GetString(row, "user_registration_vc", "user_registration", "UserRegistration"),
                        DateRegistration = GetNullableDateTime(row, "date_registration_dt", "date_registration", "DateRegistration") ?? DateTime.MinValue,
                        UserModification = GetString(row, "user_modification_vc", "user_modification", "UserModification"),
                        DateModification = GetNullableDateTime(row, "date_modification_dt", "date_modification", "DateModification") ?? DateTime.MinValue,
                        State = GetBool(row, "state_bt", "state", "State")
                    };

                    result.Add(item);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al obtener customers: {ex.Message}");
                return null!;
            }
        }
        public async Task<List<GetServiceProviderResponse>> GetServiceProviderAsync(getProviderServiceRequest request)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new("@ProviderId", request.ProviderId)
                };

                var dt = await _dataAccess.ExecuteSP_DataTable("payments.sp_GetServiceProviders", parameters);

                var result = new List<GetServiceProviderResponse>();

                if (dt == null || dt.Rows.Count == 0)
                    return result;

                // helpers locales
                string GetString(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                            return r[n]?.ToString() ?? string.Empty;
                    }
                    return string.Empty;
                }

                Guid GetGuid(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                        {
                            if (Guid.TryParse(r[n].ToString(), out var g))
                                return g;
                        }
                    }
                    return Guid.Empty;
                }

                int GetInt(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                        {
                            if (int.TryParse(r[n].ToString(), out var i))
                                return i;
                        }
                    }
                    return 0;
                }

                DateTime? GetNullableDateTime(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                        {
                            if (DateTime.TryParse(r[n].ToString(), out var d))
                                return d;
                        }
                    }
                    return null;
                }

                bool GetBool(DataRow r, params string[] names)
                {
                    foreach (var n in names)
                    {
                        if (dt.Columns.Contains(n) && r[n] != DBNull.Value)
                        {
                            if (bool.TryParse(r[n].ToString(), out var b))
                                return b;
                            if (int.TryParse(r[n].ToString(), out var i))
                                return i != 0;
                        }
                    }
                    return false;
                }

                foreach (DataRow row in dt.Rows)
                {
                    var item = new GetServiceProviderResponse
                    {
                        ProviderId = GetGuid(row, "provider_id_ui", "provider_id", "ProviderId"),
                        Name = GetString(row, "name_vc", "name", "Name"),
                        Address = GetString(row, "adress_vc", "address", "Address"),
                        Nit = GetInt(row, "nit_in", "nit", "Nit"),
                        CellPhone = GetInt(row, "cell_phone_in", "cell_phone", "CellPhone"),
                        ServiceType = GetString(row, "service_type_vc", "service_type", "ServiceType"),
                        Email = string.IsNullOrWhiteSpace(GetString(row, "email_vc", "email", "Email")) ? null : GetString(row, "email_vc", "email", "Email"),
                        UserRegistration = GetString(row, "user_registration_vc", "user_registration", "UserRegistration"),
                        DateRegistration = GetNullableDateTime(row, "date_registration_dt", "date_registration", "DateRegistration") ?? DateTime.MinValue,
                        UserModification = GetString(row, "user_modification_vc", "user_modification", "UserModification"),
                        DateModification = GetNullableDateTime(row, "date_modification_dt", "date_modification", "DateModification") ?? DateTime.MinValue,
                        State = GetBool(row, "state_bt", "state", "State")
                    };

                    result.Add(item);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al obtener service providers: {ex.Message}");
                return null!;
            }
        }
    }
}




