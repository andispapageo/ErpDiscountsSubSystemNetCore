using Domain.Core.Entities.Base;

namespace Domain.Core.Entities
{
    public partial class TbField : BaseEntity
    {
        public int ViewId { get; set; }
        public string Name { get; set; } = null!;

        public virtual TbView View { get; set; } = null!;
    }
}
