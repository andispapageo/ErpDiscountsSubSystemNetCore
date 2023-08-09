namespace Domain.Core.Entities
{
    public partial class TbOrderDiscount
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DiscountId { get; set; }
        public virtual TbOrder Order { get; set; } = null!;
        public virtual TbDiscount Discount { get; set; } = null!;
    }
}
