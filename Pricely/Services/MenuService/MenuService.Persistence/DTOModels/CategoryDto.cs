using System.Collections.Generic;

namespace MenuService.Persistence.DTOModels
{
    public class CategoryDto : EntityDtoBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ItemDto> Items { get; set; }
    }
}