using FluentValidation;

namespace Application.Shared.Commands.Orders
{
    public class OrderCommandValidator : AbstractValidator<InheritorPresenterCommand>
    {
        public OrderCommandValidator()
        {
            RuleFor(v => v.CustomerId).Must(x => x.HasValue && x.Value > 0);
        }
    }
}
