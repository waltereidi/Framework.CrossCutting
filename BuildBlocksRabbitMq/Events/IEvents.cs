using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildBlocksRabbitMq.Events
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : IntegrationEvent;
    }
}
