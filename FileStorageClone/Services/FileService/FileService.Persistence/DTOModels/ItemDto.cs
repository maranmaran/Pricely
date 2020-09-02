using System.Collections.Generic;

namespace ItemService.Persistence.DTOModels
{
    public class ItemDto : EntityDtoBase
    {
        public string Name { get; set; }
        public bool Active { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }
        public string PicturePath { get; set; }

        public CategoryDto Category { get; set; }

        public IEnumerable<IngredientDto> Ingredients { get; set; }
        public IEnumerable<AllergenDto> Allergens { get; set; }
    }
}
