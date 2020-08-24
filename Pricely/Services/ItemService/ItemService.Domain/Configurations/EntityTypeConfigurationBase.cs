using ItemService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemService.Domain.Configurations
{
    public abstract class EntityTypeConfigurationBase<TEntityBase> : IEntityTypeConfiguration<TEntityBase> where TEntityBase : EntityBase
    {
        public void Configure(EntityTypeBuilder<TEntityBase> builder)
        {
            builder.Property(x => x.DateCreated)
                .HasDefaultValueSql("getdateutc()");

            builder.Property(x => x.DateModified)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAddOrUpdate();

            ConfigureEntity(builder);
        }

        public abstract void ConfigureEntity(EntityTypeBuilder<TEntityBase> builder);
    }
}