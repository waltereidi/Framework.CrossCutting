using Microsoft.Extensions.Configuration;

using System.IO;

namespace BuildBlocksRabbitMq.Configuration
{

    public class ConfigurationFactory
    {
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
    }
}
