using System;
using DataAccess.Sql.Models;

namespace ItemService.Domain.Entities
{
    public class ItemAllergen : EntityBase
    {
        public Guid ItemId { get; set; }
        public virtual Item Item { get; set; }

        public Guid AllergenId { get; set; }
        public virtual Allergen Allergen { get; set; }
    }
}