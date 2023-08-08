using System;
using System.Collections.Generic;

namespace Domain.Core.Entities
{
    public partial class TbOrder
    {
        public TbOrder()
        {
            TbOrderDiscounts = new HashSet<TbOrderDiscount>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<TbOrderDiscount> TbOrderDiscounts { get; set; }
    }
}
