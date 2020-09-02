using System;

namespace ItemService.Persistence.DTOModels
{
    public abstract class EntityDtoBase
    {
        public Guid Id { get; set; }
    }
}