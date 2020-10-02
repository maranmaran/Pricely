using System.Collections.Generic;
using DataAccess.Sql.Models;

namespace ItemService.Domain.Entities
{
    public class Ingredient : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ItemIngredient> Items { get; set; } = new HashSet<ItemIngredient>();
    }
}