using Application.Shared.Commands;
using Application.Shared.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Core.Entities;
using Domain.Core.Enums;
using Domain.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Shared.Handlers
{
    internal class OrderDiscountHanlder : IRequestHandler<OrderDiscountsCommand, IEnumerable<OrderVm>>
    {
        private readonly IUnitOfWork<TbOrder> unitOfWork;
        private readonly IMapper mapper;

        public OrderDiscountHanlder(IUnitOfWork<TbOrder> unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<OrderVm>> Handle(OrderDiscountsCommand request, CancellationToken cancellationToken)
        {
            var orderRes = await unitOfWork.Repository.GetQuery(x => x.CustomerId == request.CustomerId, includeProperties: "Customer,TbOrderDiscounts")
                .ProjectTo<OrderVm>(mapper.ConfigurationProvider)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            foreach(var item in orderRes)
                item.FinalPrice = item.Discounts.OrderBy(x => x.PriorityOrderId).Aggregate(340M, (x, y) =>
                {
                    if (y.DiscountType == DiscountTypeEn.Percentage.ToString())
                    {
                        var discount = (x * y.Price) / 100;
                        x -= discount;
                    }
                    else if (y.DiscountType == DiscountTypeEn.Coupon.ToString())
                    {
                        x -= y.Price;
                    }
                    return x;
                });

            return orderRes;
        }
    }
}
