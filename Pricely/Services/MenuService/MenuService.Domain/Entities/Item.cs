using System.Collections.Generic;

namespace MenuService.Domain.Entities
{
    public class Item : EntityBase
    {
        public string Name { get; set; }
        public bool Active { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }
        public string PicturePath { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; } = new HashSet<Ingredient>();
        public virtual ICollection<Allergen> Allergens { get; set; } = new HashSet<Allergen>();
    }
}
