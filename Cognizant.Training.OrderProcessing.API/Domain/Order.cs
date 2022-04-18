namespace Cognizant.Training.OrderProcessing.API.Domain
{
    public class Order
    {
        public string Id { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
        public IEnumerable<OrderLineItem> LineItems { get; set; } = new List<OrderLineItem>();
    }
}
