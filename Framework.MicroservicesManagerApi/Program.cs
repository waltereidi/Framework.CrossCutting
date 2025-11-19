//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System.Text;

//var factory = new ConnectionFactory { 
//    HostName = "rabbitmq",
//    Password = "admin",
//    UserName = "admin",

//};
//using var connection = await factory.CreateConnectionAsync();
//using var channel = await connection.CreateChannelAsync();

//await channel.ExchangeDeclareAsync(exchange: "logs",
//    type: ExchangeType.Fanout);

//// declare a server-named queue
//QueueDeclareOk queueDeclareResult = await channel.QueueDeclareAsync();
//string queueName = queueDeclareResult.QueueName;
//await channel.QueueBindAsync(queue: queueName, exchange: "logs", routingKey: string.Empty);

//Console.WriteLine(" [*] Waiting for logs.");

//var consumer = new AsyncEventingBasicConsumer(channel);
//consumer.ReceivedAsync += (model, ea) =>
//{
//    byte[] body = ea.Body.ToArray();
//    var message = Encoding.UTF8.GetString(body);
//    Console.WriteLine($" [x] {message}");
//    return Task.CompletedTask;
//};

//await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);

//Console.WriteLine(" Press [enter] to exit.");
//Console.ReadLine();
using Framework.MicroservicesManagerApi.DependencyInjection;
using RabbitMQ.Client;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}


//using var connection = await factory.CreateConnectionAsync();
var channel = await RabbitMqConnectionSingleton.CreateChannelAsync();

await channel.ExchangeDeclareAsync(exchange: "LivrosExpo", type: ExchangeType.Fanout);
await channel.ExchangeDeclareAsync(exchange: "FileStorage", type: ExchangeType.Fanout);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

