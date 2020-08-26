using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.Domain.Configurations
{
    public class ItemConfiguration : EntityTypeConfigurationBase<Item>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Item> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(250);
            builder.Property(x => x.PicturePath).HasMaxLength(250);
            builder.Property(x => x.PictureUrl).HasMaxLength(1000);

            builder
                .HasMany(x => x.Ingredients)
                .WithOne(x => x.Item)
                .HasForeignKey(x => x.ItemId);

            builder
                .HasMany(x => x.Allergens)
                .WithOne(x => x.Item)
                .HasForeignKey(x => x.ItemId);

            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
