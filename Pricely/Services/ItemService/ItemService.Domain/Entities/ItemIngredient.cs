using System;

namespace ItemService.Domain.Entities
{
    public class ItemIngredient : EntityBase
    {
        public Guid ItemId { get; set; }
        public virtual Item Item { get; set; }

        public Guid IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}