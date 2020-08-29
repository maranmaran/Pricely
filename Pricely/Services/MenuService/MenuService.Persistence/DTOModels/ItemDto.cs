using System.Collections.Generic;

namespace MenuService.Persistence.DTOModels
{
    public class ItemDto : EntityDtoBase
    {
        public string Name { get; set; }
        public bool Active { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }
        public string PicturePath { get; set; }


        public IEnumerable<IngredientDto> Ingredients { get; set; }
        public IEnumerable<AllergenDto> Allergens { get; set; }
    }
}
