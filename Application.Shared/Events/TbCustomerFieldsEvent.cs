using Domain.Core.Common;
using Domain.Core.Entities;

namespace Application.Shared.Events
{
    internal class TbCustomerFieldsEvent : BaseEvent
    {
        public TbCustomerFieldsEvent(TbView item) => Item = item;
        public TbView Item { get; }
    }
}
