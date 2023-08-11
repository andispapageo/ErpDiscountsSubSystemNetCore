using Application.Shared.Commands.DynamicFields.Customer;
using Application.Shared.Commands.DynamicFields.Main;
using Application.Shared.Events;
using Application.Shared.ViewModels;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Handlers.DynamicFields.Customer
{
    internal class CustomerPostDynamicFieldsHistoryCommandHandler : IRequestHandler<CustomerPostDynamicHistoryFieldsCommand, Result>
    {
        private readonly IUnitOfWork<TbView> UowTbView;
        private readonly IUnitOfWork<TbCustomer> UowTbCustomer;
        private readonly IUnitOfWork<TbCustomerField> UowTbCustomerField;
        private readonly IUnitOfWork<TbCustomerFieldsHistory> UowCustomerFieldsHistory;

        public CustomerPostDynamicFieldsHistoryCommandHandler(
            IUnitOfWork<TbView> uowTbView,
            IUnitOfWork<TbCustomer> uowTbCustomer,
             IUnitOfWork<TbCustomerField> uowTbCustomerField,
            IUnitOfWork<TbCustomerFieldsHistory> uowCustomerFieldsHistory)
        {
            UowTbView = uowTbView;
            UowTbCustomer = uowTbCustomer;
            this.UowTbCustomerField = uowTbCustomerField;
            UowCustomerFieldsHistory = uowCustomerFieldsHistory;
        }



        public async Task<Result> Handle(CustomerPostDynamicHistoryFieldsCommand request, CancellationToken cancellationToken)
        {
            foreach (var item in request.CustomerFieldsVms)
            {
                var customerFieldExist = UowTbCustomerField.Repository.GetQuery(x => x.CustomerId == item.CustomerId && x.ViewId == item.ViewId && string.Equals(item.ViewValue, x.ViewValue)).FirstOrDefault();
                if (customerFieldExist == null) continue;
                var addHistoryValue = await UowCustomerFieldsHistory.Repository.InsertOrUpdate(new TbCustomerFieldsHistory()
                {
                    CustomerId = item.CustomerId,
                    ViewId = item.ViewId,
                    OldViewValue = customerFieldExist.ViewValue,
                    NewViewValue = item.ViewValue,
                    UpdateDate = DateTime.Now
                });

                customerFieldExist.ViewValue = item.ViewValue;
                await UowTbCustomerField.Repository.UpdateAsync(customerFieldExist);

            }

            return new Result(false, default);
        }
    }
}
