using MassTransit;
using MongoDB.Driver;
using Order.API.Consumers;
using Shared.OrderEvents;
using Shared.Settings;
using Stock.API.Consumers;
using Stock.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(configurator =>
{

    configurator.AddConsumer<OrderCompletedEventConsumer>();
    configurator.AddConsumer<StockRollbackConsumer>();

    configurator.UsingRabbitMq((context, _configure) =>
    {
        _configure.Host(builder.Configuration["RabbitMQ"]);

        _configure.ReceiveEndpoint(RabbitMQSettings.Order_OrderCompletedEventQueue, e => e.ConfigureConsumer<OrderCompletedEventConsumer>(context));
        _configure.ReceiveEndpoint(RabbitMQSettings.Order_OrderFailedEventQueue, e => e.ConfigureConsumer<StockRollbackConsumer>(context));


    });
});

builder.Services.AddSingleton<MongoDbService>();


using var scope = builder.Services.BuildServiceProvider().CreateScope();
var mongoDbService = scope.ServiceProvider.GetRequiredService<MongoDbService>();
if (!await (await mongoDbService.GetCollection<Stock.API.Models.Stock>().FindAsync(x => true)).AnyAsync())
{
    mongoDbService.GetCollection<Stock.API.Models.Stock>().InsertOne(
    new()
    {
            ProductId = 1,
            Count = 200
    });

    mongoDbService.GetCollection<Stock.API.Models.Stock>().InsertOne(
    new()
    {
        ProductId = 2,
        Count = 300
    });

    mongoDbService.GetCollection<Stock.API.Models.Stock>().InsertOne(
    new()
    {
        ProductId = 3,
        Count = 50
    });


    mongoDbService.GetCollection<Stock.API.Models.Stock>().InsertOne(
    new()
    {
        ProductId = 4,
        Count = 10
    });

    mongoDbService.GetCollection<Stock.API.Models.Stock>().InsertOne(
    new()
    {
         ProductId = 5,
         Count = 60
    });
}


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

app.Run();

