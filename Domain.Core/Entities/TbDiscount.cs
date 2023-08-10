using Domain.Core.Entities.Base;

namespace Domain.Core.Entities
{
    public partial class TbDiscount : BaseEntity
    {
        public int DiscountTypeId { get; set; }
        public string DiscountName { get; set; } = null!;
        public decimal Price { get; set; }
        public int CurrencyId { get; set; }
        public int PriorityOrderId { get; set; }

        public virtual TbCurrency Currency { get; set; } = null!;
        public virtual TbDiscountType DiscountType { get; set; } = null!;
        public virtual ICollection<TbOrderDiscount> TbOrderDiscounts { get; set; }
    }
}
