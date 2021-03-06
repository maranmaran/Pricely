﻿using DataAccess.Sql.Models;
using ItemService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ItemService.Domain.Configurations
{
    public class ItemAllergenConfiguration : EntityTypeConfigurationBase<ItemAllergen>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ItemAllergen> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey(x => new { x.AllergenId, x.ItemId });

            builder
                .HasOne(x => x.Item)
                .WithMany(x => x.Allergens)
                .HasForeignKey(x => x.ItemId);

            builder
                .HasOne(x => x.Allergen)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.AllergenId);
        }
    }
}