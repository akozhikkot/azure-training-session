using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleReceiver
{
    internal class SimpleReceiver
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceBusClient _client;

        public SimpleReceiver(
            IConfiguration configuration, 
            ServiceBusClient client)
        {
            _configuration = configuration;
            _client = client;
        }

        public async Task ReceiveMessages()
        {
            var receiver = _client.CreateReceiver(_configuration.GetValue<string>("QueueName"));
            
            while (true)
            {
                var message = await receiver.ReceiveMessageAsync();

                if (message != null)
                {
                    Console.WriteLine($"Received message: {message.Body}");
                    await receiver.CompleteMessageAsync(message);
                }
                else
                {
                    Console.WriteLine("All messages received");
                    break;
                }
            } 
            
            await receiver.CloseAsync();
        }
    }
}
