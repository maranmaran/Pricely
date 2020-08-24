using System;
using ItemService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemService.Domain.Configurations
{
    public class ItemIngredientConfiguration : EntityTypeConfigurationBase<ItemIngredient>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ItemIngredient> builder)
        {
            throw new NotImplementedException();
        }
    }
}