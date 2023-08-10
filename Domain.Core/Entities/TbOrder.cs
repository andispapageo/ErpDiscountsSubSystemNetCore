using Domain.Core.Entities.Base;

namespace Domain.Core.Entities
{
    public partial class TbOrder : BaseEntity
    {
        public TbOrder()
        {
            TbOrderDiscounts = new HashSet<TbOrderDiscount>();
            TbSubscriptions = new HashSet<TbSubscription>();
        }

        public int ProductId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CustomerId { get; set; }
        public virtual TbCustomer Customer { get; set; } = null!;
        public virtual ICollection<TbOrderDiscount> TbOrderDiscounts { get; set; }
        public virtual ICollection<TbSubscription> TbSubscriptions { get; set; }
    }
}
