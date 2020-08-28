using AutoMapper;
using IdentityService.Domain.Entities;
using IdentityService.Persistence.DTOModels;

namespace IdentityService.Business
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Company, CompanyDto>().ReverseMap();
        }
    }
}
