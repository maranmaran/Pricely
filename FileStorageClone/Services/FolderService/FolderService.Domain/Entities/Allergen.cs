using System.Collections.Generic;

namespace ItemService.Domain.Entities
{
    public class Allergen : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ItemAllergen> Items { get; set; } = new HashSet<ItemAllergen>();
    }
}