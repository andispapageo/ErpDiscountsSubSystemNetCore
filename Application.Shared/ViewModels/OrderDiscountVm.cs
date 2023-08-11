using AutoMapper;
using Domain.Core.Entities;

namespace Application.Shared.ViewModels
{
    public class OrderDiscountVm
    {
        public int OrderId { get; set; }
        public int DiscountTypeId { get; set; }
        public string DiscountName { get; set; } = null!;
        public string DiscountType { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal DeductedPrice { get; set; }
        public string? Currency { get; set; }
        public string? CurrencySymbol { get; set; }
        public int PriorityOrderId { get; set; }


        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<TbOrderDiscount, OrderDiscountVm>()
                    .ForMember(d => d.OrderId, opt => opt.MapFrom(s => s.OrderId))
                    .ForMember(d => d.DiscountTypeId, opt => opt.MapFrom(s => s.Discount.DiscountTypeId))
                    .ForMember(d => d.DiscountType, opt => opt.MapFrom(s => s.Discount.DiscountType.DiscountType))
                    .ForMember(d => d.DiscountName, opt => opt.MapFrom(s => s.Discount.DiscountName))
                    .ForMember(d => d.Currency, opt => opt.MapFrom(s => s.Discount.Currency.Name))
                    .ForMember(d => d.CurrencySymbol, opt => opt.MapFrom(s => s.Discount.Currency.Symbol))
                    .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Discount.Price))
                    .ForMember(d => d.PriorityOrderId, opt => opt.MapFrom(s => s.Discount.PriorityOrderId));
            }
        }
    }
}
