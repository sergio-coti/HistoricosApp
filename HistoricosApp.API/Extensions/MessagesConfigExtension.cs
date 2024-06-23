using HistoricosApp.API.Consumers;
using HistoricosApp.API.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HistoricosApp.API.Extensions
{
    public static class MessagesConfigExtension
    {
        public static IServiceCollection AddMessagesConfig
            (this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQSettings = new RabbitMQSettings();
            new ConfigureFromConfigurationOptions<RabbitMQSettings>
                (configuration.GetSection("RabbitMQ")).Configure(rabbitMQSettings);

            services.AddSingleton(rabbitMQSettings);

            services.AddSingleton<IConnectionFactory>(cf =>
            {
                var config = cf.GetRequiredService<RabbitMQSettings>();
                return new ConnectionFactory
                {
                    HostName = config.Hostname,
                    Port = config.Port,
                    UserName = config.Username,
                    Password = config.Password,
                    VirtualHost = config.Username
                };
            });

            services.AddHostedService<MessageConsumer>();

            return services;
        }
    }
}
