using Microsoft.Extensions.Diagnostics.HealthChecks;
using PAY.CROSS.DATAACCESS;

namespace ServicePaymentsAPI.Healthcheks
{
    public class DataBaseHealthchecks : IHealthCheck
    {
        private readonly IDataAccess access;
        private readonly DBOptionItems config;
        public DataBaseHealthchecks(IDataAccess access, DBOptionItems item)
        {
            this.access = access;
            config = item;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var check = await access.Check(config.name);
                if (check.Item1)
                {
                    return HealthCheckResult.Healthy(check.Item2);
                }
                return HealthCheckResult.Unhealthy(check.Item2);
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(ex.Message);
            }
        }
    }
}
