namespace Cognizant.Training.OrderProcessing.API.Dto
{
    public record OrderLineItemDto(
        int ProductId,
        int Units,
        decimal UnitPrice,
        decimal Discount);
}
