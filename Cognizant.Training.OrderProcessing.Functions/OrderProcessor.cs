using Cognizant.Training.OrderProcessing.Functions.Events;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Cognizant.Training.OrderProcessing.Functions
{
    public class OrderProcessor
    {
        private readonly ILogger _logger;

        public OrderProcessor(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<OrderProcessor>();
        }

        [Function("OrderProcessor")]
        [BlobOutput("orders/{rand-guid}-output.txt", Connection = "OrdersStorage")]
        public string Run([ServiceBusTrigger("%OrderProcessingQueue%", Connection = "MessageBus")] string orderCreatedMessage)
        {
            var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(orderCreatedMessage);

            if (orderCreatedEvent == null)
            {
                _logger.LogError("Invalid Message received from Service Bus - {message}", orderCreatedMessage);
                return "Invalid Message received from Service Bus";
            }

            // Some processing logic here
            _logger.LogInformation("Order {OrderId} processed", orderCreatedEvent.OrderId);

            return orderCreatedMessage;
        }
    }
}
