namespace Cognizant.Training.OrderProcessing.API.Dto
{
    public record CreateOrderDto(
            string CustomerName,
            string AddressLine1,
            string AddressLine2,
            string City,
            string PostCode,
            IEnumerable<OrderLineItemDto> LineItems);
}
