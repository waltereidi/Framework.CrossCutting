using BuildBlocksRabbitMq.Configuration;
using BuildBlocksRabbitMq.Enum;
using BuildBlocksRabbitMq.Events;
using BuildBlocksRabbitMq.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Reflection.Metadata;
using NLog;
using NLog.Web;

namespace BuildBlocksRabbitMq
{
    public static class RabbitMQExtensions
    {
        //private static ConnectionFactory GetConnectionFactory(IConfiguration configuration)
        //{
        //    var hostname = configuration["RabbitMQ:HostName"]
        //        ?? configuration["RabbitMQ:Host"]
        //        ?? "localhost";
        //    var username = configuration["RabbitMQ:UserName"];
        //    var password = configuration["RabbitMQ:Password"];

        //    var factory = new ConnectionFactory
        //    {
        //        HostName = hostname,
        //        AutomaticRecoveryEnabled = true,
        //        NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
        //        RequestedHeartbeat = TimeSpan.FromSeconds(60),
        //        UserName = string.IsNullOrWhiteSpace(username) ? ConnectionFactory.DefaultUser : username,
        //        Password = string.IsNullOrWhiteSpace(password) ? ConnectionFactory.DefaultPass : password
        //    };
        //    return factory;
        //}
        
        public static async Task<IServiceCollection> AddRabbitMQProducer( this IServiceCollection services , RabbitMqQueueName queueName )
        {

            var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

            // Register RabbitMQ Connection as singleton
            services.AddSingleton(async (sp) =>
            {
                //var logger = sp.GetRequiredService<ILogger<IConnection>>();
                logger.Info("Starting application...");
                IConnectionFactory factory = ConfigurationFactory.GetConnectionFactory();
                ConfigurationFactory.GetConfiguration();
                try
                {
                    var connection = await factory.CreateConnectionAsync();
                    logger.Info(
                        "✅ [event-bus] Conexão RabbitMQ estabelecida - Host: {Host}",
                        ConfigurationFactory.GetConfiguration().GetHostName() );
                    return connection;
                }
                catch (Exception ex)
                {
                    logger.Error(ex,
                        "❌ [event-bus] Falha ao estabelecer conexão RabbitMQ em {Host}",
                        ConfigurationFactory.GetConfiguration().GetHostName());
                    throw;
                }
            });

            //Register Producer
            services.AddSingleton<RabbitMQProducer>((sp) =>
            {
                //var logger = sp.GetRequiredService<ILogger<RabbitMQProducer>>();
                //var hostname = configuration["RabbitMQ:HostName"]
                //                ?? configuration["RabbitMQ:Host"]
                //                ?? "localhost";
                //var exchange = configuration["RabbitMQ:ExchangeName"]
                //                ?? configuration["RabbitMQ:Exchange"]
                //                ?? "ecommerce.events";
                //var username = configuration["RabbitMQ:UserName"];
                //var password = configuration["RabbitMQ:Password"];

                return new RabbitMQProducer();
            });

            // Register EventBus interface
            services.AddSingleton<IEventBus>(sp =>
            {
                var producer = sp.GetRequiredService<RabbitMQProducer>();
                //return new RabbitMQEventBus(producer);
                return null;
            });

            // Add Health Check
            //services.AddHealthChecks()
            //    .AddCheck<RabbitMQHealthCheck>(
            //        "rabbitmq",
            //        tags: new[] { "ready", "messaging" });

            return services;
        }



        //private void EnsureConnection()
        //{
        //    if (_connection?.IsOpen == true && _channel?.IsOpen == true)
        //        return;

        //    lock (_lock)
        //    {
        //        if (_connection?.IsOpen == true && _channel?.IsOpen == true)
        //            return;

        //        try
        //        {
        //            _connection?.Dispose();
        //            _channel?.Dispose();

        //            _connection = _factory.CreateConnection();
        //            _channel = _connection.CreateModel();

        //            _channel.ExchangeDeclare(
        //                exchange: _exchangeName,
        //                type: ExchangeType.Topic,
        //                durable: true);

        //            _logger.LogInformation(
        //                "✅ [event-bus] Produtor RabbitMQ conectado - Exchange: {Exchange}",
        //                _exchangeName);
        //        }
        //        catch (BrokerUnreachableException ex)
        //        {
        //            _logger.LogError(ex,
        //                "❌ [event-bus] Falha ao conectar ao RabbitMQ em {HostName}",
        //                _factory.HostName);
        //            throw;
        //        }
        //    }
        //}
    }
}
