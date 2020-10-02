using System;

namespace DataAccess.NoSql.Models
{
    public abstract class DocumentBase : IDocument
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; } = DateTime.UtcNow;
    }
}