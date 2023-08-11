using Domain.Core.Entities.Base;

namespace Domain.Core.Entities
{
    public partial class TbView : BaseEntity
    {
        public TbView()
        {
            TbCustomerFields = new HashSet<TbCustomerField>();
            TbFields = new HashSet<TbField>();
        }

        public int TypeId { get; set; }
        public string Name { get; set; } = null!;
        public virtual TbViewType Type { get; set; } = null!;
        public virtual ICollection<TbCustomerField> TbCustomerFields { get; set; }
        public virtual ICollection<TbField> TbFields { get; set; }
    }
}
