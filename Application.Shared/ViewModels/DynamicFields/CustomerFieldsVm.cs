using AutoMapper;
using Domain.Core.Entities;

namespace Application.Shared.ViewModels.DynamicFields
{
    public class CustomerFieldsVm
    {
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int ViewId { get; set; }
        public string? ViewName { get; set; }
        public string? ViewValue { get; set; }
        public int ViewTypeId { get; set; }
        public ICollection<TbField>? DropDownFields { get; set; }

        public class Mapping : Profile
        {
            public Mapping()
            {
                //IQueryable
                CreateMap<TbCustomerField, CustomerFieldsVm>()
                    .ForMember(d => d.CustomerId, opt => opt.MapFrom(s => s.CustomerId))
                    .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer.Name))
                    .ForMember(d => d.ViewId, opt => opt.MapFrom(s => s.View.Id))
                    .ForMember(d => d.ViewName, opt => opt.MapFrom(s => s.View.Name))
                    .ForMember(d => d.ViewTypeId, opt => opt.MapFrom(s => s.View.Type.Id))
                    .ForMember(d => d.DropDownFields, opt => opt.MapFrom(s => s.View.TbFields));


                //IEnumerable
                CreateMap<TbCustomerField, CustomerFieldsVm>()
                    .ForMember(d => d.CustomerId, opt => opt.MapFrom(s => s.CustomerId))
                    .ForMember(d => d.CustomerName, opt => opt.MapFrom(s => s.Customer.Name))
                    .ForMember(d => d.ViewId, opt => opt.MapFrom(s => s.View.Id))
                    .ForMember(d => d.ViewName, opt => opt.MapFrom(s => s.View.Name))
                    .ForMember(d => d.ViewTypeId, opt => opt.MapFrom(s => s.View.Type.Id))
                    .ForMember(d => d.DropDownFields, opt => opt.MapFrom(s => s.View.TbFields));

            }
        }
    }
}
