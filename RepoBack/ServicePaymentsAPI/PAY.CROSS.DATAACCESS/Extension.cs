using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PAY.CROSS.DATAACCESS
{
    public static class Extension
    {
        private static readonly string dataBaseSectionName = "database";

        public static IServiceCollection AddDataBase(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            DBOptions optionsDB = new DBOptions();
            configuration.GetSection(dataBaseSectionName).Bind(optionsDB);
            services.AddSingleton<IDataAccess, DataAccess>(sp =>
            {
                return new DataAccess(optionsDB);
            });
            return services;
        }
    }
}
