using Application.Shared.ViewModels;
using Application.Shared.ViewModels.DynamicFields;
using Application.Shared.ViewModels.TextAreaFields;
using MediatR;

namespace Application.Shared.Commands.DynamicFields.Customer
{
    public class CustomerPostDynamicHistoryFieldsCommand : IRequest<Result>
    {
       public IEnumerable<CustomerFieldsVm>  CustomerFieldsVms { get; set; }
    }
}
