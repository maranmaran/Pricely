using System;
using ItemService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemService.Domain.Configurations
{
    public class CategoryConfiguration : EntityTypeConfigurationBase<Category>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Category> builder)
        {
            throw new NotImplementedException();
        }
    }
}