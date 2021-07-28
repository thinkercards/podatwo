using AutoMapper;
using CCMERP.Domain.Entities;
using CCMERP.Infrastructure.ViewModel;

namespace CCMERP.Infrastructure.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerModel, Customer>()
                .ForMember(dest => dest.CustomerID,
                        opt => opt.MapFrom(src => src.CustomerId))
                .ReverseMap();
        }
    }
}
