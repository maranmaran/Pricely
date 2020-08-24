using System;

namespace IdentityService.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
    }
}
