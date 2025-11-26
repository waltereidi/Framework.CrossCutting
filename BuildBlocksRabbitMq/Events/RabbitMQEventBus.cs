using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildBlocksRabbitMq.Events
{
    public class RabbitMQEventBus : IEventBus
    {
        //private readonly RabbitMQExtensions _producer;

        //public RabbitMQEventBus(RabbitMQExtensions producer)
        //{
        //    _producer = producer;
        //}

        //public Task PublishAsync<T>(T @event) where T : IntegrationEvent
        //{
        //    return _producer.PublishAsync(@event);
        //}
        public Task PublishAsync<T>(T @event) where T : IntegrationEvent
        {
            throw new NotImplementedException();
        }
    }
}
