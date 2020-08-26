using System;

namespace IdentityService.Persistence.DTOModels
{
    public abstract class EntityDtoBase
    {
        public Guid Id { get; set; }
    }
}