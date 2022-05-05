// See https://aka.ms/new-console-template for more information
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Scripts;
using Newtonsoft.Json;

var endpoint = "<<EndpointHere>>";
var accountKey = "<<AccountKeyHere>>";
var database = "<<DbName>>";
var containerName = "<<ContainerName>>";

var cosmosClient = new CosmosClient(
        endpoint,
        accountKey);

var container = cosmosClient.GetContainer(database, containerName);
//await CreateStoredProcedure(container, "spHelloWorld");
//await CreateStoredProcedure(container, "spCreateOrderWithAmount");
//await CreateUDF(container, "calculateTotalAmount");

//await ExecuteStoredProcedure(container, "spHelloWorld", string.Empty);
//await ExecuteCreateOrder(container, "spCreateOrderWithAmount");


Console.WriteLine("All done");
Console.ReadLine();


static async Task CreateUDF(Container container, string functionName)
{
    var functiontext = File.ReadAllText($@"Functions\{functionName}.js");
    var props = new UserDefinedFunctionProperties()
    {
        Id = functionName,
        Body = functiontext
    };
    await container.Scripts.CreateUserDefinedFunctionAsync(props);
}

static async Task CreateStoredProcedure(Container container, string spName)
{
    var spText = File.ReadAllText($@"StoredProcs\{spName}.js");
    var props = new StoredProcedureProperties(spName, spText);
    var result = await container.Scripts.CreateStoredProcedureAsync(props);
}

static async Task ExecuteStoredProcedure(Container container, string spName, string partitionKey)
{
    var partitionKeyValue = new PartitionKey(partitionKey);
    var result = await container.Scripts.ExecuteStoredProcedureAsync<string>(
        spName, partitionKeyValue, null);
    Console.WriteLine(result.Resource);
}

static async Task ExecuteCreateOrder(Container container, string spName)
{
    var count = 100;
    var partitionKey = 1000;
    var orderToCreate = new
    {
        id = $"Order-{Guid.NewGuid()}",
        LineItems = new[]
        {
            new
            {
                ProductId = $"count",
                Name = $"Product-{count}",
                NumberofItems = 1,
                UnitPrice = count
            }
        },
        Customer = new
        {
            Name = $"Customer-{count}",
            Address = new
            {
                Line1 = $"Address-{count}",
                PostCode = partitionKey
            }
        }
    };

    var partitionKeyValue = new PartitionKey(partitionKey);
    var result = 
        await container.Scripts.ExecuteStoredProcedureAsync<dynamic>(
            spName, partitionKeyValue, new[] { orderToCreate });
    Console.WriteLine(result.Resource);
}
