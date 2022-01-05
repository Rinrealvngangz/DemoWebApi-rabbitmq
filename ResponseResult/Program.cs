using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Core;
using ResponseResult;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureServices((_, service) =>
{
    service.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@localhost:5672"));
    service.AddSingleton<ISubscribe<OrderResponse>, Subscriber<OrderResponse>>();
    service.AddTransient<IRepositoryOrder, CheckoutOrder>();
    service.AddHostedService<ResponseData>();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();