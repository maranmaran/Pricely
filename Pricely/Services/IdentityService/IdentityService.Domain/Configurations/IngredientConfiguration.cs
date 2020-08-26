using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Domain.Configurations
{
    public class IngredientConfiguration : EntityTypeConfigurationBase<Ingredient>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Ingredient> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(250);

            builder
                .HasMany(x => x.Items)
                .WithOne(x => x.Ingredient)
                .HasForeignKey(x => x.IngredientId);
        }
    }
}