using Framework.MicroservicesManagerApi.Interfaces;
using NLog.Web;
using RabbitMQ.Client;
using System.ComponentModel.DataAnnotations;

namespace Framework.MicroservicesManagerApi.Publishers
{
    public abstract class RabbitMQPublisher : IRabbitMQPublisher
    {
        protected string _queueName;
        protected IChannel _channel; 
        public RabbitMQPublisher(IChannel channel , string queueName )
        { 
            _queueName = queueName;
            _channel = channel;
        }
        public virtual void PublishAsync(IRabbitMQMessage m)
        {
            try
            {
                _channel.BasicPublishAsync(
                exchange: GetExchange(),
                routingKey: _queueName,
                body: m.GetContent() );
            }
            catch (Exception ex)
            {
                var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
                logger.Info("Starting application...");
            }
        }
        protected virtual string GetExchange()
            => string.Empty;

        
    }
}
