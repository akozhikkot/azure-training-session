using Azure.Core;
using Azure.Data.Tables;
using Azure.Identity;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Cognizant.Training.StorageAccount.API.Config;
using Cognizant.Training.StorageAccount.API.Repository;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<StorageConfig>(builder.Configuration.GetSection("StorageConfig"));

builder.Services.AddSingleton<CustomerQueueRepository>((provider) =>
{
    var storageConfig = provider.GetRequiredService<IOptions<StorageConfig>>();
    var queueClient = new QueueClient(
    new Uri($"https://{storageConfig.Value.AccountName}.queue.core.windows.net/{storageConfig.Value.QueueName}"),
    new StorageSharedKeyCredential(storageConfig.Value.AccountName, storageConfig.Value.AccessKey));
    return new CustomerQueueRepository(queueClient);
});

builder.Services.AddSingleton<CustomerTableStorageRepository>((provider) =>
{
    var storageConfig = provider.GetRequiredService<IOptions<StorageConfig>>();
    var tableClient = new TableClient(
    new Uri($"https://{storageConfig.Value.AccountName}.table.core.windows.net"),
    storageConfig.Value.TableName,
    new TableSharedKeyCredential(storageConfig.Value.AccountName, storageConfig.Value.AccessKey));
    return new CustomerTableStorageRepository(tableClient);
});

builder.Services.AddSingleton<BlobRepository>((provider) =>
{
    var storageConfig = provider.GetRequiredService<IOptions<StorageConfig>>();
    var blobClient = new BlobContainerClient(
    new Uri($"https://{storageConfig.Value.AccountName}.blob.core.windows.net/{storageConfig.Value.ContainerName}"),
    new StorageSharedKeyCredential(storageConfig.Value.AccountName, storageConfig.Value.AccessKey));
    return new BlobRepository(blobClient);
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

app.Run();
