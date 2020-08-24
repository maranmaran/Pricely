using System;
using ItemService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemService.Domain.Configurations
{
    public class AllergenConfiguration : EntityTypeConfigurationBase<Allergen>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Allergen> builder)
        {
            throw new NotImplementedException();
        }
    }
}