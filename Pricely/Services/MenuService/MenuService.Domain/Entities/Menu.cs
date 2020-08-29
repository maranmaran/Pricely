using System;
using System.Collections.Generic;

namespace MenuService.Domain.Entities
{
    public class Menu : EntityBase
    {
        public string Name { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
    }
}
