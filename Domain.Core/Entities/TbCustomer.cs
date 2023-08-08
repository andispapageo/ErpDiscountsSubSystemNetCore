using System;
using System.Collections.Generic;

namespace Domain.Core.Entities
{
    public partial class TbCustomer
    {
        public TbCustomer()
        {
            TbOrderDiscounts = new HashSet<TbOrderDiscount>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<TbOrderDiscount> TbOrderDiscounts { get; set; }
    }
}
