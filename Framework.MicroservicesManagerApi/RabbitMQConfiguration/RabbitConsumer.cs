using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Microsoft.Extensions.Options;
using Framework.MicroservicesManagerApi.DTO;

namespace Framework.MicroservicesManagerApi.RabbitMQConfiguration
{

    public class RabbitConsumer : BackgroundService
    {
        private readonly RabbitMQSettings _settings;
        private readonly ConnectionFactory _factory;

        public RabbitConsumer(IOptions<RabbitMQSettings> options)
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // conexão aceita cancellation token
            var connection = await _factory.CreateConnectionAsync(stoppingToken);

            // channel NÃO aceita token
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: _settings.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            // Novo handler de mensagens no channel
            channel.BasicDeliver += async (sender, args) =>
            {
                if (stoppingToken.IsCancellationRequested)
                    return;

                var msg = Encoding.UTF8.GetString(args.Body.ToArray());
                Console.WriteLine($"Mensagem recebida: {msg}");

                await channel.BasicAckAsync(args.DeliveryTag, false);
            };

            // iniciar o consumo
            await channel.BasicConsumeAsync(
                queue: _settings.QueueName,
                autoAck: false
            );

            // manter serviço vivo
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
