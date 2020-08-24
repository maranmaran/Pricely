using System;
using ItemService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemService.Domain.Configurations
{
    public class IngredientConfiguration : EntityTypeConfigurationBase<Ingredient>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Ingredient> builder)
        {
            throw new NotImplementedException();
        }
    }
}