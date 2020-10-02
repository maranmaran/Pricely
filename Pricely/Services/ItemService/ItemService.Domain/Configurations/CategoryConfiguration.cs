using DataAccess.Sql.Models;
using ItemService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemService.Domain.Configurations
{
    public class CategoryConfiguration : EntityTypeConfigurationBase<Category>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(250);

            builder
                .HasMany(x => x.Items)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}