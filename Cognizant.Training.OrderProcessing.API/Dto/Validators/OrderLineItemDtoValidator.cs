using FluentValidation;

namespace Cognizant.Training.OrderProcessing.API.Dto.Validators
{
    public class OrderLineItemDtoValidator : AbstractValidator<OrderLineItemDto>
    {
        public OrderLineItemDtoValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage($"Valid {nameof(OrderLineItemDto.ProductId)} should be specified");
            RuleFor(x => x.Units).GreaterThan(0).WithMessage($"Atleast there should be one Unit of purchase");
            RuleFor(x => x.Discount).LessThanOrEqualTo(100).WithMessage($"Cannot apply a discount more than 100%");
        }
    }
}
