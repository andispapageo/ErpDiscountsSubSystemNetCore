using System;
using System.Collections.Generic;

namespace Domain.Core.Entities
{
    public partial class TbCurrency
    {
        public TbCurrency()
        {
            TbDiscounts = new HashSet<TbDiscount>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Symbol { get; set; } = null!;

        public virtual ICollection<TbDiscount> TbDiscounts { get; set; }
    }
}
