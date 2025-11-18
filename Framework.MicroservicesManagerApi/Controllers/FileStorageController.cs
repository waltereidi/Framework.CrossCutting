using Framework.MicroservicesManagerApi.DependencyInjection;
using Framework.MicroservicesManagerApi.DTO;
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
        private readonly IChannel _connection;
        public FileStorageController(ILogger<FileStorageController> logger)
        {
            _logger = logger;
            _connection = RabbitMqConnectionSingleton.CreateChannelAsync().Result;
        }
        public GenericResponse RequestFile()
        {
            return new GenericResponse();
        }
    }
}
