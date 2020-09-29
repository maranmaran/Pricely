using DataAccess.Sql.Entities;
using System.Collections.Generic;

namespace ItemService.Domain.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Item> Items { get; set; } = new HashSet<Item>();
    }
}