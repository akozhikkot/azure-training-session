namespace RetailApp.Domain
{
    public class OrderItem
    {
        public string ProductId { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal NumberofItems { get; set; }

    }
}