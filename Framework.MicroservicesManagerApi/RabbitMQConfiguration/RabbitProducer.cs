using Framework.MicroservicesManagerApi.DTO;
using Framework.MicroservicesManagerApi.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace Framework.MicroservicesManagerApi.RabbitMQConfiguration
{

    public class RabbitProducer : IRabbitProducer
    {
        private readonly RabbitMQSettings _settings;
        private readonly ConnectionFactory _factory;

        public RabbitProducer(IOptions<RabbitMQSettings> options)
        {
            _settings = options.Value;

            _factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password,
                VirtualHost = _settings.VirtualHost
            };
        }

        public async Task PublishAsync(string message)
        {
            await using var connection = await _factory.CreateConnectionAsync(); // novo método
            await using var channel = await connection.CreateChannelAsync();     // IChannel no lugar de IModel

            await channel.QueueDeclareAsync(
                queue: _settings.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: _settings.QueueName,
                mandatory: false,
                body: body
            );
        }
    }

}
