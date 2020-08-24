using System;
using ItemService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemService.Domain.Configurations
{
    public class ItemConfiguration : EntityTypeConfigurationBase<Item>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Item> builder)
        {
            throw new NotImplementedException();
        }
    }
}
