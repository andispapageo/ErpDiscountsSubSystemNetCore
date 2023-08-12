using Application.Shared.ViewModels.DynamicFields;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using FluentValidation;

namespace Application.Shared.Commands.DynamicFields.Customer
{
    public class CustomerPostDynamicFieldsCommandValidator : AbstractValidator<CustomerPostDynamicHistoryFieldsCommand>
    {
        public CustomerPostDynamicFieldsCommandValidator(IUnitOfWork<TbCustomer> uowCustomer, IUnitOfWork<TbView> uowView)
        {
            UowCustomer = uowCustomer;
            UowView = uowView;

            //RuleFor(v => v.CustomerFieldsVms).NotEmpty().NotNull();
            //RuleFor(v => v.CustomerFieldsVms.FirstOrDefault()).NotNull().Must(x => x.ViewTypeId != default);
            //RuleFor(v => v.CustomerFieldsVms.FirstOrDefault()).NotNull().Must(x => x.DropDownFields?.Any() ?? false);

            //RuleFor(v => v.CustomerFieldsVms.FirstOrDefault()).Must(CustomerExists)
            //   .WithMessage("'Customer Id not exists.")
            //   .WithErrorCode("Unique");

            //RuleFor(v => v.CustomerFieldsVms.FirstOrDefault()).Must(ViewExists)
            //  .WithMessage("'View Id not exists.")
            //  .WithErrorCode("Unique");
        }

        readonly IUnitOfWork<TbCustomer> UowCustomer;
        readonly IUnitOfWork<TbView> UowView;

        public bool CustomerExists(CustomerFieldsVm? customerFields) => UowCustomer.Repository.GetQuery(x => x.Id == customerFields.CustomerId)?.Any() ?? false;
        public bool ViewExists(CustomerFieldsVm? customerFields) => UowView.Repository.GetQuery(x => x.Id == customerFields.ViewId)?.Any() ?? false;
    }
}
