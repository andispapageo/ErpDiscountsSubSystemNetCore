using Domain.Core.Entities.Base;

namespace Domain.Core.Entities
{
    public partial class TbDiscountType : BaseEntity
    {
        public TbDiscountType()
        {
            TbDiscounts = new HashSet<TbDiscount>();
        }

        public string DiscountType { get; set; } = null!;
        public virtual ICollection<TbDiscount> TbDiscounts { get; set; }
    }
}
