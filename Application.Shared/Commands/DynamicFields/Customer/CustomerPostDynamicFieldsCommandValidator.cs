using FluentValidation;

namespace Application.Shared.Commands.DynamicFields.Customer
{
    public class CustomerPostDynamicFieldsCommandValidator : AbstractValidator<CustomerPostDynamicFieldsCommand>
    {

        public CustomerPostDynamicFieldsCommandValidator()
        {

            RuleFor(v => v.DynamicFieldsPostVm).NotEmpty().NotNull();

        }
    }
}
