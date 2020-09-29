using DataAccess.Sql.Entities;
using ItemService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemService.Domain.Configurations
{
    public class ItemIngredientConfiguration : EntityTypeConfigurationBase<ItemIngredient>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ItemIngredient> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey(x => new { x.IngredientId, x.ItemId });

            builder
                .HasOne(x => x.Item)
                .WithMany(x => x.Ingredients)
                .HasForeignKey(x => x.ItemId);

            builder
                .HasOne(x => x.Ingredient)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.IngredientId);
        }
    }
}