using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;

namespace Domain.Core.Entities
{
    public partial class TbCustomerFieldsHistory : BaseEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ViewId { get; set; }
        public string? OldViewValue { get; set; }
        public string? NewViewValue { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual TbCustomer Customer { get; set; } = null!;
        public virtual TbView View { get; set; } = null!;
    }
}
