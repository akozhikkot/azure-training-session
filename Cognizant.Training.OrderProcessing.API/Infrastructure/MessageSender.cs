using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace Cognizant.Training.OrderProcessing.API.Infrastructure
{
    public class MessageSender : IMessageSender
    {
        private readonly IConfiguration _configuration;
        public MessageSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Send<T>(T message)
        {
            var serviceBusConnectionString = _configuration.GetConnectionString("MessageBus");
            var queueName = _configuration.GetValue<string>("OrderProcessingQueue");
            var client = new ServiceBusClient(serviceBusConnectionString);
            var sender = client.CreateSender(queueName);
            var messages = new ServiceBusMessage[] { new ServiceBusMessage(JsonSerializer.Serialize(message)) };
            await sender.SendMessagesAsync(messages);
        }
    }
}
