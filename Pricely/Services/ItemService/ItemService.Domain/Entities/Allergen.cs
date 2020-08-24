using System;

namespace ItemService.Domain.Entities
{
    public class Allergen : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid ItemAllergenId { get; set; }
        public virtual ItemAllergen ItemAllergen { get; set; }
    }
}