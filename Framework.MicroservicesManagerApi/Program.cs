using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
builder.Services.AddSingleton<IModel, IModel>();
ConnectionFactory factory = new ConnectionFactory();
// "guest"/"guest" by default, limited to localhost connections
factory.UserName = "root";
factory.Password = "root";
factory.VirtualHost = ConnectionFactory.DefaultVHost;
factory.HostName = "localhost";
factory.Port = AmqpTcpEndpoint.UseDefaultPort;

IConnection conn = await factory.CreateConnectionAsync();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
