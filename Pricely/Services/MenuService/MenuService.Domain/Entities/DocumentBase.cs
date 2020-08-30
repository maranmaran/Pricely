using MenuService.Domain.Interfaces;
using System;

namespace MenuService.Domain.Entities
{
    public abstract class DocumentBase : IDocument
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; } = DateTime.UtcNow;
    }
}