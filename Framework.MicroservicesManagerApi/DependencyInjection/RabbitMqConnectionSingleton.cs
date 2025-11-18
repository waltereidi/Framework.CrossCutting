using RabbitMQ.Client;
using System;
using System.Threading.Tasks;

namespace Framework.MicroservicesManagerApi.DependencyInjection
{
    public sealed class RabbitMqConnectionSingleton
    {
        private static readonly Lazy<Task<IConnection>> _lazyConnection =
            new Lazy<Task<IConnection>>(CreateConnection);

        private static ConnectionFactory _factory= new ConnectionFactory
        {
            HostName = "rabbitmq",
            Password = "admin",
            UserName = "admin",
        };

        private RabbitMqConnectionSingleton() { }


        private static async Task<IConnection> CreateConnection()
        {
            if (_factory == null)
                throw new InvalidOperationException("RabbitMqConnectionSingleton must be configured before use.");

            return await _factory.CreateConnectionAsync();
        }

        public static async Task<IChannel> CreateChannelAsync()
        {
            var connection = await _lazyConnection.Value;
            return await connection.CreateChannelAsync();
        }
    }
}
