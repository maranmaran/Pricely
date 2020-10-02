using DataAccess.NoSql.Attributes;
using System;
using System.Linq;

namespace DataAccess.NoSql.Helpers
{
    public static class DocumentHelper
    {

        public static string GetCollectionName(this Type document)
        {
            return ((BsonCollectionAttribute)document.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }
    }
}
