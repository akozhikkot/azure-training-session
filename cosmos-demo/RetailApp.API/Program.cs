using Azure.Cosmos;
using Microsoft.Extensions.Options;
using RetailApp.API.Config;
using RetailApp.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DataStoreConfig>(builder.Configuration.GetSection("DataStoreConfig"));

builder.Services.AddSingleton<OrderRepository>((sp) =>
{
    var dataStoreConfig = sp.GetRequiredService<IOptions<DataStoreConfig>>();
    var cosmosClient = new CosmosClient(
        dataStoreConfig.Value.Endpoint,
        dataStoreConfig.Value.AccountKey);
    return new OrderRepository(cosmosClient, dataStoreConfig);
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
