using System;
using System.Collections.Generic;

namespace ItemService.Domain.Entities
{
    public class Item : EntityBase
    {
        public string Name { get; set; }
        public bool Active { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }
        public string PicturePath { get; set; }

        public Guid TypeId { get; set; }
        public virtual Category Type { get; set; }

        public virtual ICollection<ItemIngredient> Ingredients { get; set; }
        public virtual ICollection<ItemAllergen> Allergens { get; set; }
    }
}
