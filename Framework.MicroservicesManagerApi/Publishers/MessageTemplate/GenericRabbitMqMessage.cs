using Framework.MicroservicesManagerApi.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Framework.MicroservicesManagerApi.Publishers.MessageTemplate
{
    public abstract class GenericRabbitMqMessage: IRabbitMQMessage 
    {
        [JsonInclude]
        [JsonPropertyName("eventName")]
        protected abstract string EventName {  get; set; }
        [JsonInclude]
        [JsonPropertyName("eventVersion")]
        protected abstract string EventVersion { get; set; }
        [JsonInclude]
        [JsonPropertyName("timeStamp")]
        protected DateTime TimeStamp { get; set; }
        [JsonInclude]
        [JsonPropertyName("data")]
        protected abstract object Data { get; set; }
        public GenericRabbitMqMessage()
        {
            TimeStamp = DateTime.Now;
        }
        public GenericRabbitMqMessage(DateTime timeStamp )
        {
            TimeStamp = timeStamp;
        }

        protected virtual string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                IncludeFields = true
            };

            var json = JsonSerializer.Serialize(this , options);
            return json;
        }
        public byte[] GetContent()
        {
            return System.Text.Encoding.UTF8.GetBytes(ToJson());
        }
    }
}
