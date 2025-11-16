namespace Framework.MicroservicesManagerApi.Interfaces
{
    public interface IRabbitProducer
    {
        Task PublishAsync(string message);
    }
}
