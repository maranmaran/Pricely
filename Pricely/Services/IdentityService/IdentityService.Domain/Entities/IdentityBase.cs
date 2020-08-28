using System;

namespace IdentityService.Domain.Entities
{
    public interface IEntity
    {
    }

    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
    }

}
