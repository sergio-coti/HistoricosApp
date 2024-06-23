using HistoricosApp.API.Contexts;
using HistoricosApp.API.Settings;
using Microsoft.Extensions.Options;

namespace HistoricosApp.API.Extensions
{
    public static class DatabaseConfigExtension
    {
        public static IServiceCollection AddDatabaseConfig
            (this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDBSettings = new MongoDBSettings();
            new ConfigureFromConfigurationOptions<MongoDBSettings>
                (configuration.GetSection("MongoDB")).Configure(mongoDBSettings);

            services.AddSingleton(mongoDBSettings);
            services.AddTransient<MongoDBContext>();

            return services;
        }
    }
}
