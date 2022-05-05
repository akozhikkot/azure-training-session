using Azure.Cosmos;
using Microsoft.Extensions.Options;
using RetailApp.API.Config;
using RetailApp.Domain;

namespace RetailApp.API.Repository
{
    public class OrderRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly IOptions<DataStoreConfig> _options;

        public OrderRepository(CosmosClient cosmosClient, IOptions<DataStoreConfig> options)
        {
            _cosmosClient = cosmosClient;
            _options = options;
        }
        
        private CosmosContainer GetOrdersContainer() =>
            _cosmosClient.GetContainer(_options.Value.DatabaseId, _options.Value.ContainerId);

        public async Task BulkInsert(int orderIdStart)
        {
            var counter = orderIdStart;
            var tasks = new List<Task>(counter);
            CosmosContainer ordersContainer = GetOrdersContainer();
            while (counter > (orderIdStart - 10))
            {
                Order order = CreateOrder(counter);
                var insertItemTask = 
                    ordersContainer.CreateItemAsync(order, new PartitionKey(order.Customer.Address.PostCode));

                tasks.Add(insertItemTask.ContinueWith(t =>
                {
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        Console.WriteLine($"Order {order.Id} inserted");
                    }
                    else
                    {
                        Console.WriteLine($"Order {order.Id} failed to insert {t.Exception?.Message}");
                    }
                }));

                counter--;
            }

            await Task.WhenAll(tasks);
        }

        private Order CreateOrder(int count)
        {
            return new Order
            {
                Id = $"Order-{count}",
                LineItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        ProductId = $"count",
                        Name = $"Product-{count}",
                        NumberofItems = 1,
                        UnitPrice = count
                    }
                },
                Customer = new Customer
                {
                    Name = $"Customer-{count}",
                    Address = new Address
                    {
                        Line1 = $"Address-{count}",
                        PostCode = $"200{count}"
                    }
                }
            };
        }
        public async Task<Order> Create(Order order)
        {
            order.Id = Guid.NewGuid().ToString();
            CosmosContainer ordersContainer = GetOrdersContainer();
            ItemResponse<Order> createOrderResponse = await ordersContainer.CreateItemAsync<Order>(order);
            return createOrderResponse.Value;
        }
        public async Task<List<Order>> Query(string name)
        {
            var sqlQueryText = $"SELECT * FROM c WHERE c.Customer.Name = '{name}'";
            CosmosContainer ordersContainer = GetOrdersContainer();
            List<Order> orders = new List<Order>();
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
        
            await foreach(var item in ordersContainer.GetItemQueryIterator<Order>(queryDefinition))
            {
                orders.Add(item);
            }             
            
            return orders;
        }
        public async Task<Order> Replace(string id, string postCode)
        {
            CosmosContainer ordersContainer = GetOrdersContainer();
            ItemResponse<Order> getOrderByIdResponse = await ordersContainer.ReadItemAsync<Order>(id, new PartitionKey(postCode));
            Order order = getOrderByIdResponse.Value;
            order.Customer.Name = order.Customer.Name + " Updated";
            await ordersContainer.ReplaceItemAsync<Order>(order, order.Id, new PartitionKey(postCode));            
            return order;
        }
        public async Task Delete(string id, string postCode)
        {
            CosmosContainer ordersContainer = GetOrdersContainer();
            ItemResponse<Order> deleteByIdResponse = 
                await ordersContainer.DeleteItemAsync<Order>(id, new PartitionKey(postCode));           
        }
    }
}
