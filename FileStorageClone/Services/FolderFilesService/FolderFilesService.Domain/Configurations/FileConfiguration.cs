using FolderFilesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FolderFilesService.Domain.Configurations
{
    public class FileConfiguration : EntityTypeConfigurationBase<File>
    {
        public override void ConfigureEntity(EntityTypeBuilder<File> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(250);

            builder
                .HasOne(x => x.ParentFolder)
                .WithMany(x => x.Files)
                .OnDelete(DeleteBehavior.SetNull);
        }    
    }
}