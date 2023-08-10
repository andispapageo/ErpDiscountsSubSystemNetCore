using Domain.Core.Entities.Base;

namespace Domain.Core.Entities
{
    public partial class TbSubscription : BaseEntity
    {
        public int OrderId { get; set; }
        public string SubscriptionType { get; set; } = null!;
        public decimal Price { get; set; }
        public int DatePlan { get; set; }
        public int DateNum { get; set; }

        public virtual TbOrder Order { get; set; } = null!;
    }
}
