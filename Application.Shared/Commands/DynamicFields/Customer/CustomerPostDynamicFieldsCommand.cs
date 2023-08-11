using Application.Shared.ViewModels;
using Application.Shared.ViewModels.TextAreaFields;
using MediatR;

namespace Application.Shared.Commands.DynamicFields.Customer
{
    public class CustomerPostDynamicFieldsCommand : IRequest<Result>
    {
        public DynamicFieldsPostVm? DynamicFieldsPostVm { get; init; }
    }
}
