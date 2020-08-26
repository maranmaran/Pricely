using System.Collections.Generic;

namespace IdentityService.Domain.Entities
{
    public class Ingredient : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ItemIngredient> Items { get; set; } = new HashSet<ItemIngredient>();
    }
}