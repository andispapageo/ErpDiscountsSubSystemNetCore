namespace Domain.Core.Entities
{
    public partial class TbDiscountType
    {
        public TbDiscountType()
        {
            TbDiscounts = new HashSet<TbDiscount>();
        }

        public int Id { get; set; }
        public string DiscountType { get; set; } = null!;

        public virtual ICollection<TbDiscount> TbDiscounts { get; set; }
    }
}
