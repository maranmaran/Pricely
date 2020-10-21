using AutoMapper;
using ItemService.API.Protos;
using ItemService.Persistence.DTOModels;

namespace ItemService.API.GrpcServices
{
    public class GrpcMappings : Profile
    {
        public GrpcMappings()
        {
            CreateMap<AllergenDto, Allergen>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.Id.ToString()))
                .ReverseMap();
        }
    }
}