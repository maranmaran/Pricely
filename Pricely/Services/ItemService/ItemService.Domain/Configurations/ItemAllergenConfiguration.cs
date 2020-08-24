using System;
using ItemService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemService.Domain.Configurations
{
    public class ItemAllergenConfiguration : EntityTypeConfigurationBase<ItemAllergen>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ItemAllergen> builder)
        {
            throw new NotImplementedException();
        }
    }
}