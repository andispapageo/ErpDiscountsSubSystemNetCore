using Application.Shared.Commands.Orders;
using Application.Shared.ViewModels;
using Application.Shared.ViewModels.DynamicFields;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Shared.Handlers.Orders
{
    internal class OrderCommandHandler : IRequestHandler<InheritorPresenterCommand, InheritorPresenterVm>
    {
        readonly IUnitOfWork<TbCustomerField> uowCustomerField;
        readonly IUnitOfWork<TbOrder> uowOrder;
        readonly IMapper mapper;

        public OrderCommandHandler(IUnitOfWork<TbOrder> uowOrder,
                                   IUnitOfWork<TbCustomerField> uowCustomerField,
                                   IMapper mapper)
        {
            this.uowOrder = uowOrder;
            this.uowCustomerField = uowCustomerField;
            this.mapper = mapper;
        }

        public async Task<InheritorPresenterVm> Handle(InheritorPresenterCommand request, CancellationToken cancellationToken)
        {
            var orderRes = await uowOrder.Repository.GetQuery(x => x.CustomerId == request.CustomerId, includeProperties: "Customer,TbOrderDiscounts")
                .ProjectTo<OrderVm>(mapper.ConfigurationProvider)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            foreach (var item in orderRes.Where(x => x != null && (x.Discounts?.Any() ?? false) && (x.Subscriptions?.Any() ?? false)))
                item.FinalPrice = item.Discounts.OrderBy(x => x.PriorityOrderId).Aggregate(item.Subscriptions.FirstOrDefault().Price, (x, y) =>
                {
                    if (y.DiscountType == DiscountTypeEn.Percentage.ToString())
                    {
                        var discount = x * y.Price / 100;
                        x -= discount;
                    }
                    else if (y.DiscountType == DiscountTypeEn.Coupon.ToString())
                    {
                        x -= y.Price;
                    }
                    return x;
                });

            var customerFields = await uowCustomerField.Repository.GetQuery(x => x.CustomerId == request.CustomerId)
                .ProjectTo<CustomerFieldsVm>(mapper.ConfigurationProvider)
                .ToListAsync();

            var homeVm = new InheritorPresenterVm()
            {
                OrderVms = orderRes,
                CustomerFields = customerFields
            };
            return homeVm;
        }
    }
}
