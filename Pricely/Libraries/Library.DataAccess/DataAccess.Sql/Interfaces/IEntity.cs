using System;

namespace DataAccess.Sql.Interfaces
{
    /// <summary>
    /// Base interface for SQL entities
    /// </summary>
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
    }
}
