namespace Cognizant.Training.OrderProcessing.API.Dto
{
    public record OrderDto(
            string Id,
            string CustomerName,
            string AddressLine1,
            string AddressLine2,
            string City,
            string PostCode,
            IEnumerable<OrderLineItemDto> LineItems);
}
