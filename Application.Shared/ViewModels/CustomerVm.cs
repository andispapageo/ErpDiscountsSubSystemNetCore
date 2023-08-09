using AutoMapper;
using Domain.Core.Entities;

namespace Application.Shared.ViewModels
{
    public class CustomerVm
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; } = null!;

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<TbCustomer, CustomerVm>();
            }
        }
    }
}
