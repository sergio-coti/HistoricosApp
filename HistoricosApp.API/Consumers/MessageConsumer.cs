using HistoricosApp.API.Contexts;
using HistoricosApp.API.Models;
using HistoricosApp.API.Settings;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace HistoricosApp.API.Consumers
{
    public class MessageConsumer : BackgroundService
    {
        private readonly IConnectionFactory? _connectionFactory;
        private readonly RabbitMQSettings? _settings;
        private readonly MongoDBContext? _mongoDBContext;
        private readonly IServiceProvider _serviceProvider;

        public MessageConsumer(IConnectionFactory? connectionFactory, RabbitMQSettings? settings, MongoDBContext? mongoDBContext, IServiceProvider serviceProvider)
        {
            _connectionFactory = connectionFactory;
            _settings = settings;
            _mongoDBContext = mongoDBContext;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connection = _connectionFactory?.CreateConnection();
            var model = connection?.CreateModel();

            model.QueueDeclare(queue: _settings?.Queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(model);
            consumer.Received += (sender, args) =>
            {
                var content = Encoding.UTF8.GetString(args.Body.ToArray());
                var logProdutos = JsonConvert.DeserializeObject<LogProdutos>(content);

                #region Gravar o log no mongodb

                using (var scope = _serviceProvider.CreateScope())
                {
                    _mongoDBContext?.Produtos.InsertOne(logProdutos);                  
                }

                model.BasicAck(args.DeliveryTag, false); //removendo da fila

                #endregion
            };

            //configurando e executando a leitura
            model.BasicConsume(_settings.Queue, false, consumer);
            return Task.CompletedTask;
        }
    }
}
