using System.Text.Json.Serialization;

namespace RetailApp.Domain
{
    public class Order
    {
        [JsonPropertyName("id")]        
        public string Id { get; set; }
        public List<OrderItem> LineItems { get; set; }
        public Customer Customer { get; set; }
        public Order()
        {
            LineItems = new List<OrderItem>();
        }
    }
}