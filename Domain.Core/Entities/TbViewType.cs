namespace Domain.Core.Entities
{
    public partial class TbViewType
    {
        public TbViewType()
        {
            TbViews = new HashSet<TbView>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; } = null!;

        public virtual ICollection<TbView> TbViews { get; set; }
    }
}
