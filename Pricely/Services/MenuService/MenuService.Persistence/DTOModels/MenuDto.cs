using System;
using System.Collections.Generic;
using DataAccess.NoSql.Models;

namespace MenuService.Persistence.DTOModels
{
    public class MenuDto : DocumentDtoBase
    {
        public string Name { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}