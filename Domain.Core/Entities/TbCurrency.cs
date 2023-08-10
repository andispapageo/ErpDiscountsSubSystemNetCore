using Domain.Core.Entities.Base;

namespace Domain.Core.Entities
{
    public partial class TbCurrency : BaseEntity
    {
        public TbCurrency()
        {
            TbDiscounts = new HashSet<TbDiscount>();
        }

        public string Name { get; set; } = null!;
        public string Symbol { get; set; } = null!;

        public virtual ICollection<TbDiscount> TbDiscounts { get; set; }
    }
}
