using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildBlocksRabbitMq.Producer
{
    public class RabbitMQProducer : IDisposable
    {
        private readonly string _exchangeName;
        private readonly object _lock = new();
        private IConnection? _connection;
        private bool _disposed;
        private readonly ConnectionFactory _factory;
        private ILogger _logger;
        public RabbitMQProducer()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
