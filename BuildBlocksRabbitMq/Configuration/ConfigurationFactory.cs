using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.IO;
using System.Runtime.CompilerServices;

namespace BuildBlocksRabbitMq.Configuration
{

    public static class ConfigurationFactory
    {
        public static string GetHostName(this IConfiguration config)
            => config["RabbitMQ:HostName"] ?? throw new ArgumentException(nameof(config));
        public static string GetUserName(this IConfiguration config)
            => config["RabbitMQ:UserName"] ?? throw new ArgumentException(nameof(config));
        public static string GetPassword(this IConfiguration config)
            => config["RabbitMQ:HostName"] ?? throw new ArgumentException(nameof(config));

        public static IConfiguration GetConfiguration()
        {
            var basePath = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.Parent.FullName, "BuildBlocksRabbitMq");
            // Build a configuration object from JSON file
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            // Get a configuration section

            return config;
        }
        public static ConnectionFactory GetConnectionFactory()
        {
            var hostname = GetConfiguration().GetHostName();
            var username = GetConfiguration().GetUserName();
            var password = GetConfiguration().GetPassword();

            var factory = new ConnectionFactory
            {
                HostName = hostname,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
                RequestedHeartbeat = TimeSpan.FromSeconds(60),
                UserName = string.IsNullOrWhiteSpace(username) ? ConnectionFactory.DefaultUser : username,
                Password = string.IsNullOrWhiteSpace(password) ? ConnectionFactory.DefaultPass : password
            };
            return factory;
        }
      
    }
    
    
}
