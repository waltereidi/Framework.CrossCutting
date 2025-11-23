using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class RabbitMqConsumerService : BackgroundService
{
    private readonly ConnectionFactory _factory;

    public RabbitMqConsumerService()
    {
        _factory = new ConnectionFactory
        {
            HostName = "rabbitmq",
            UserName = "admin",
            Password = "admin"
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Conexão
        var connection = await _factory.CreateConnectionAsync();
        // Canal
        var channel = await connection.CreateChannelAsync();

        // Declarar fila
        await channel.QueueDeclareAsync(
            queue: "FileStorage",
            durable: true,
            exclusive: false,
            autoDelete: false
        );

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (sender, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"📩 Nova mensagem recebida: {message}");

            // Confirmação manual
            await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
        };

        Console.WriteLine(" [*] Aguardando mensagens...");

        // Inicia o consumo
        await channel.BasicConsumeAsync(
            queue: "FileStorage",
            autoAck: false,
            consumer: consumer
        );

        // Mantém o service ativo
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}
