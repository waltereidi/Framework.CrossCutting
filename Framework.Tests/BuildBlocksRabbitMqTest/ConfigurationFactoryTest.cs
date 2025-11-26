using BuildBlocksRabbitMq.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.BuildBlocksRabbitMqTest
{

    public class ConfigurationFactoryTest
    {
        [Fact]
        public void TestConfigurationFactory()
        {
            ConfigurationFactory.Create();
        }
    }
}
