namespace Domain.Core.Entities
{
    public partial class TbOrder
    {
        public TbOrder()
        {
            TbOrderDiscounts = new HashSet<TbOrderDiscount>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CustomerId { get; set; }
        public virtual TbCustomer Customer { get; set; } = null!;
        public virtual ICollection<TbOrderDiscount> TbOrderDiscounts { get; set; }
    }
}
