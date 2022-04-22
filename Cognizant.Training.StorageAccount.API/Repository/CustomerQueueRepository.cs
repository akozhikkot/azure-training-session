using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System.Text;

namespace Cognizant.Training.StorageAccount.API.Repository
{
    public class CustomerQueueRepository
    {
        private readonly QueueClient queueClient;

        public CustomerQueueRepository(QueueClient queueClient)
        {
            this.queueClient = queueClient;
        }

        public void AddMessage(string message)
        {
            queueClient.CreateIfNotExists();
            queueClient.SendMessage(message);
        }

        public List<string> GetMessages(bool peekOnly = false)
        {
            var messages = new List<string>();

            if (peekOnly)
            {
                foreach (PeekedMessage message in queueClient.PeekMessages(maxMessages: 10).Value)
                {
                    // "Process" the message
                    messages.Add(message.Body.ToString()); 
                }
            }
            else
            {
                foreach (QueueMessage message in queueClient.ReceiveMessages(maxMessages: 10).Value)
                {
                    // "Process" the message
                    messages.Add(message.Body.ToString());

                    // Let the service know we're finished with the message and
                    // it can be safely deleted.
                    queueClient.DeleteMessage(message.MessageId, message.PopReceipt);
                }
            }
            return messages;
        }
    }
}
