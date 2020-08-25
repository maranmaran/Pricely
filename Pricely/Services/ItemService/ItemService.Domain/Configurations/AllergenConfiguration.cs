using ItemService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemService.Domain.Configurations
{
    public class AllergenConfiguration : EntityTypeConfigurationBase<Allergen>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Allergen> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(250);

            builder
                .HasMany(x => x.Items)
                .WithOne(x => x.Allergen)
                .HasForeignKey(x => x.AllergenId);
        }
    }
}