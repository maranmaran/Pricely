﻿using DataAccess.Sql.Entities;
using System.Collections.Generic;

namespace ItemService.Domain.Entities
{
    public class Ingredient : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ItemIngredient> Items { get; set; } = new HashSet<ItemIngredient>();
    }
}