namespace Domain.Core.Entities
{
    public partial class TbSubscription
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Symbol { get; set; } = null!;
    }
}
