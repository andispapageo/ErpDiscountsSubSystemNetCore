using Domain.Core.Entities.Base;

namespace Domain.Core.Entities
{
    public partial class TbCustomer : BaseEntity
    {
        public TbCustomer()
        {
            TbOrders = new HashSet<TbOrder>();
        }

        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<TbOrder> TbOrders { get; set; }
    }
}
