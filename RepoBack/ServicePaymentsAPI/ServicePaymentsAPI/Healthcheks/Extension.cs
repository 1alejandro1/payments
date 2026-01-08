using Microsoft.Extensions.Diagnostics.HealthChecks;
using PAY.CROSS.DATAACCESS;

namespace ServicePaymentsAPI.Healthcheks
{
    public static class Extension
    {
        public static IServiceCollection AddHealthChecksApi(this IServiceCollection services,List<DBOptionItems> lstDatabase)
        {   
            foreach (var item in lstDatabase)
            {
                services.AddHealthChecks()
                    .AddTypeActivatedCheck<DataBaseHealthchecks>(item.name,
                        failureStatus: HealthStatus.Unhealthy,
                        tags: new[] { "DATABASE" },
                        args: new object[] { item });
            }
            return services;
        }
    }
}
