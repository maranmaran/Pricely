using System;
using DataAccess.Sql.Interfaces;

namespace DataAccess.Sql.Entities
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}