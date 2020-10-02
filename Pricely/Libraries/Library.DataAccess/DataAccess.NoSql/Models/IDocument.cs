using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess.NoSql.Models
{
    public interface IDocument
    {
        [BsonId]
        Guid Id { get; set; }

        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
    }
}
