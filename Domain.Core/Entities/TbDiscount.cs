using System;
using System.Collections.Generic;

namespace Domain.Core.Entities
{
    public partial class TbDiscount
    {
        public int Id { get; set; }
        public int DiscountTypeId { get; set; }
        public string DiscountName { get; set; } = null!;
        public decimal Price { get; set; }
        public int CurrencyId { get; set; }
        public int PriorityOrderId { get; set; }

        public virtual TbCurrency Currency { get; set; } = null!;
        public virtual TbDiscountType DiscountType { get; set; } = null!;
    }
}
