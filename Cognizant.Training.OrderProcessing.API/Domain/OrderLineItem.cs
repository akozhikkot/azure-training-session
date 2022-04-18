namespace Cognizant.Training.OrderProcessing.API.Domain
{
    public class OrderLineItem
    {
        public int ProductId { get; set; }
        public int Units { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
