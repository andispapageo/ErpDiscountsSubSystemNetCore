using AutoMapper;
using Domain.Core.Entities;

namespace Application.Shared.ViewModels
{
    public class SubscriptionVm
    {
        public int OrderId { get; set; }
        public string SubscriptionType { get; set; } = null!;
        public decimal Price { get; set; }
        public int DatePlan { get; set; }
        public int DateNum { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<TbSubscription, SubscriptionVm>();
            }
        }
    }
}
