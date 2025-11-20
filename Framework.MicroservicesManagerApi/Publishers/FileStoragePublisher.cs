using RabbitMQ.Client;

namespace Framework.MicroservicesManagerApi.Publishers
{
    public class FileStoragePublisher : RabbitMQPublisher
    {
        public FileStoragePublisher(IChannel channel) : base(channel, "FileStorage")
        {

        }

    }
}
