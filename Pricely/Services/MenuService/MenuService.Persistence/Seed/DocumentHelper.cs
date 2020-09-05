using System;
using System.Linq;
using MenuService.Domain.Attributes;

namespace MenuService.Persistence.Seed
{
    internal static class DocumentHelper
    {

        internal static string GetCollectionName(this Type document)
        {
            return ((BsonCollectionAttribute)document.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }
    }
}
