using System;
using System.Collections.Generic;

namespace Domain.Core.Entities
{
    public partial class TbOrderDiscount
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }

        public virtual TbCustomer Customer { get; set; } = null!;
        public virtual TbOrder Order { get; set; } = null!;
    }
}
