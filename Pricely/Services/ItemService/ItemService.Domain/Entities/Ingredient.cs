using System;

namespace ItemService.Domain.Entities
{
    public class Ingredient : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid ItemIngredientId { get; set; }
        public virtual ItemIngredient ItemIngredient { get; set; }
    }
}