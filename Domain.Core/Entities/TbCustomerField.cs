using Domain.Core.Entities.Base;

namespace Domain.Core.Entities
{
    public partial class TbCustomerField : BaseEntity
    {
        public int CustomerId { get; set; }
        public int ViewId { get; set; }
        public string? ViewValue { get; set; }
        public virtual TbCustomer Customer { get; set; } = null!;
        public virtual TbView View { get; set; } = null!;
    }
}
