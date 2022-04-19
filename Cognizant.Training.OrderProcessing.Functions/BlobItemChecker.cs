using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Cognizant.Training.OrderProcessing.Functions
{
    public class BlobItemChecker
    {
        private readonly ILogger _logger;

        public BlobItemChecker(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BlobItemChecker>();
        }

        [Function("BlobItemChecker")]
        public void Run([BlobTrigger("orders/{name}", Connection = "OrdersStorage")] string myTriggerItem)
        {
            _logger.LogInformation("C# Blob trigger function processed: {myTriggerItem}", myTriggerItem);
        }
    }
}
