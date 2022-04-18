namespace Cognizant.Training.OrderProcessing.API.Message
{
    public class OrderCreatedMessage : IMessage
    {
        public OrderCreatedMessage(string orderId)
        {
            OrderId = orderId;
        }

        public string OrderId { get; }
    }
}
