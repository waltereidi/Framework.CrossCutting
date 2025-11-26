using Microsoft.Extensions.Configuration;

using System.IO;

namespace BuildBlocksRabbitMq.Configuration
{

    public static class ConfigurationFactory
    {
        public static IConfiguration Create()
        {
            var builder = new ConfigurationBuilder()
            {
                BasePath = Directory.GetCurrentDirectory()
            }
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

        }
    }
}
