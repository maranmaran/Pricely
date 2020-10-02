﻿using DataAccess.Sql.Models;

namespace ItemService.Persistence.DTOModels
{
    public class CategoryDto : EntityDtoBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}