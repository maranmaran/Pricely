using System;
using System.Collections.Generic;

namespace MenuService.Domain.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Item> Items { get; set; } = new HashSet<Item>();
    }
}