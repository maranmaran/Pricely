using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MenuService.Domain.Interfaces
{
    public interface IDocument
    {
        [BsonId]
        Guid Id { get; set; }

        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
    }
}
