using MenuService.Domain.Attributes;
using System;
using System.Collections.Generic;

namespace MenuService.Domain.Entities
{
    [BsonCollection("menus")]
    public class Menu : DocumentBase
    {
        public string Name { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
    }
}
