using Domain.Core.Entities.Base;
using Domain.Core.Interfaces;
using MediatR;

namespace Domain.Core.Entities
{
    public partial class TbCustomer : BaseEntity
    {
        public TbCustomer()
        {
            TbOrders = new HashSet<TbOrder>();
            TbCustomerFields = new HashSet<TbCustomerField>();
            TbCustomerFieldsHistories = new HashSet<TbCustomerFieldsHistory>();

        }

        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public virtual ICollection<TbCustomerField> TbCustomerFields { get; set; }
        public virtual ICollection<TbCustomerFieldsHistory> TbCustomerFieldsHistories { get; set; }
        public virtual ICollection<TbOrder> TbOrders { get; set; }

        public async Task AddDynamicViews(IUnitOfWork<TbCustomerField> UowCustomerFields, int viewId, string? fieldValue = null)
        {
            if (viewId == default || UowCustomerFields == null) return;
            await UowCustomerFields.Repository.InsertOrUpdate(new TbCustomerField()
            {
                CustomerId = Id,
                ViewId = viewId,
                ViewValue = fieldValue
            });
        }
    }
}
