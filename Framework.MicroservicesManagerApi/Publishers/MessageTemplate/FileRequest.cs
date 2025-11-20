namespace Framework.MicroservicesManagerApi.Publishers.MessageTemplate
{
    public class FileRequest : GenericRabbitMqMessage
    {
        public record FromFileName(string fileId );
        public FileRequest(string fileId) : base()
        {
            Data = new FromFileName(fileId);
            EventVersion = "1";
            EventName = "FileRequest";
        }
        protected override object Data { get ; set ; }
        protected override string EventName { get ; set; }
        protected override string EventVersion { get; set; }
        
    }
}
