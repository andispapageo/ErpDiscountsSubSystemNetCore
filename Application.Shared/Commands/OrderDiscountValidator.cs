using FluentValidation;

namespace Application.Shared.Commands
{
    public class OrderDiscountValidator : AbstractValidator<OrderDiscountsCommand>
    {
        public OrderDiscountValidator()
        {
            RuleFor(v => v.CustomerId).Must(x => x.HasValue && x.Value > 0);
        }
    }
}
