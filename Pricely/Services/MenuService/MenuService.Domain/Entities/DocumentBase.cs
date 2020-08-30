using MenuService.Domain.Interfaces;
using MongoDB.Bson;
using System;

namespace MenuService.Domain.Entities
{
    public abstract class DocumentBase : IDocument
    {
        public ObjectId Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}