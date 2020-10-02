using System;
using System.Collections.Generic;
using DataAccess.Sql.Models;

namespace ItemService.Domain.Entities
{
    public class Item : EntityBase
    {
        public string Name { get; set; }
        public bool Active { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }
        public string PicturePath { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<ItemIngredient> Ingredients { get; set; } = new HashSet<ItemIngredient>();
        public virtual ICollection<ItemAllergen> Allergens { get; set; } = new HashSet<ItemAllergen>();
    }
}
