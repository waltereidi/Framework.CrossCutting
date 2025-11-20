using Framework.MicroservicesManagerApi.DependencyInjection;
using Framework.MicroservicesManagerApi.DTO;
using Framework.MicroservicesManagerApi.Interfaces;
using Framework.MicroservicesManagerApi.Publishers;
using Framework.MicroservicesManagerApi.Publishers.MessageTemplate;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace Framework.AuthApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileStorageController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRabbitMQPublisher _publisher; 
        public FileStorageController(ILogger<FileStorageController> logger)
        {
            _logger = logger;
            var channel = RabbitMqConnectionSingleton.CreateChannelAsync().Result;
            _publisher = new FileStoragePublisher(channel );
        }
        [HttpGet]
        public GenericResponse RequestFile(string id )
        {
            IRabbitMQMessage message = new FileRequest( id );
            var e = message.GetContent();
            _publisher.PublishAsync(message);
            return new GenericResponse();
        }

    }
}
