using AutoMapper;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;
using System.Linq;

namespace ItemService.Business
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Allergen, AllergenDto>().ReverseMap();
            CreateMap<Ingredient, IngredientDto>().ReverseMap();

            // map between item entities
            // maps same name properties
            CreateMap<Item, ItemDto>()
                .ReverseMap()

                // Ignore category virtual member.. if it has guid it will be mapped correctly in DB
                //.ForMember(x => x.CategoryId, o => o.MapFrom(x => x.Category.Id))
                .ForMember(x => x.Category, o => o.Ignore())

                // special handling for ingredients because they have many2many join entity
                .ForMember(d => d.Ingredients,
                    opt => opt.MapFrom(
                        // for each item ingredientDTO map to ItemIngredient join entity
                        p => p.Ingredients
                            .Select(a => new ItemIngredient
                            {

                                //ItemId = p.Id,
                                IngredientId = a.Id,
                                // Ignore because this will introduce issues and try to insert new item
                                // ID on JOIN entity is enough
                                //Ingredient = new Ingredient()
                                //{
                                //    Id = a.Id,
                                //    Description = a.Description,
                                //    Name = a.Name,
                                //}
                            })
                    )
                )
                // special handling for allergens because they have many2many join entity
                .ForMember(d => d.Allergens,
                opt => opt.MapFrom(
                    // for each item allergensDTO map to ItemAllergens join entity
                    p => p.Allergens
                        .Select(a => new ItemAllergen()
                        {

                            //ItemId = p.Id,
                            AllergenId = a.Id,
                            //Allergen = new Allergen()
                            //{
                            //    Id = a.Id,
                            //    Description = a.Description,
                            //    Name = a.Name,
                            //}
                        })
                )
            );

            // Turn item ingredients join entity into ingredientDTO
            // map join entity between item and it's ingredients to ingredientDTO
            // to flatten it
            CreateMap<ItemIngredient, IngredientDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IngredientId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Ingredient.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Ingredient.Description));


            // Turn item allergens join entity into allergenDTO
            // map join entity between item and it's allergens to allergenDTO
            // to flatten it
            CreateMap<ItemAllergen, AllergenDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.AllergenId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Allergen.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Allergen.Description));
        }
    }
}
