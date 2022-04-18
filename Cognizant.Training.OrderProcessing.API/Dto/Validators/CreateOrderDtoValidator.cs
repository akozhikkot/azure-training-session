using FluentValidation;

namespace Cognizant.Training.OrderProcessing.API.Dto.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator(IValidator<OrderLineItemDto> lineItemValidator)
        {
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage($"{nameof(CreateOrderDto.CustomerName)} can not be empty");
            RuleFor(x => x.AddressLine1).NotEmpty().WithMessage($"{nameof(CreateOrderDto.AddressLine1)} can not be empty");
            RuleFor(x => x.PostCode).NotEmpty().WithMessage($"{nameof(CreateOrderDto.PostCode)} can not be empty");

            When(x => x.LineItems != null && x.LineItems.Any(), () =>
            {
                RuleForEach(x => x.LineItems).SetValidator(lineItemValidator);
            });
        }
    }
}
