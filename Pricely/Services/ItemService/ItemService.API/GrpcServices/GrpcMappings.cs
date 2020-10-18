using AutoMapper;
using ItemService.API.Protos;
using ItemService.Persistence.DTOModels;

namespace ItemService.API.Controllers
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<AllergenDto, Allergen>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.Id.ToString()))
                .ReverseMap();
        }
    }
}