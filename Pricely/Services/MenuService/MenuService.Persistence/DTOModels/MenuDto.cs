using System;
using System.Collections.Generic;

namespace MenuService.Persistence.DTOModels
{
    public class MenuDto : EntityDtoBase
    {
        public string Name { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}