using AutoMapper;
using MenuService.Domain.Entities;
using MenuService.Persistence.DTOModels;

namespace MenuService.Business
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Allergen, AllergenDto>().ReverseMap();
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<Menu, MenuDto>().ReverseMap();
        }
    }
}
