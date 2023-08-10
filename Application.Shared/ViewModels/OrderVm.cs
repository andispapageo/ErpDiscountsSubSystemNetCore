using AutoMapper;
using Domain.Core.Entities;
using Domain.Core.Enums;

namespace Application.Shared.ViewModels
{
    public class OrderVm
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public CustomerVm? Customer { get; set; }
        public IEnumerable<OrderDiscountVm>? Discounts { get; set; }
        public decimal FinalPrice { get; set; }
        public class Mapping : Profile
        {
            public Mapping()
            {
                //IQueryable
                CreateMap<TbOrder, OrderVm>()
                    .ForMember(d => d.Customer, opt => opt.MapFrom(s => s.Customer))
                    .ForMember(d => d.Discounts, opt => opt.MapFrom(s => s.TbOrderDiscounts))
                    .AfterMap((src, dest) =>
                    {
                        dest.FinalPrice = src.TbOrderDiscounts.OrderBy(x => x.Discount.PriorityOrderId).Aggregate(360M, (x, y) =>
                        {
                            if (y.Discount.DiscountName == DiscountTypeEn.Percentage.ToString())
                            {
                                var discount = (x * y.Discount.Price) / 100;
                                x -= discount;
                            }
                            else if (y.Discount.DiscountName == DiscountTypeEn.Coupon.ToString())
                            {
                                x -= y.Discount.Price;
                            }
                            return x;
                        });
                    });

                //IEnumerable
                CreateProjection<TbOrder, OrderVm>()
                    .ForMember(d => d.Customer, opt => opt.MapFrom(s => s.Customer))
                    .ForMember(d => d.Discounts, opt => opt.MapFrom(s => s.TbOrderDiscounts));

            }
        }
    }
}
