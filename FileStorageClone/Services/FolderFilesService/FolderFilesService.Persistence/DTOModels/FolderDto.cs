using System.Collections.Generic;
using FolderFilesService.Domain.Entities;

namespace FolderFilesService.Persistence.DTOModels
{
    public class FolderDto : EntityDtoBase
    {
        public string Name { get; set; }

        public IEnumerable<FileDto> Files { get; set; }
        public IEnumerable<FolderDto> Folders { get; set; }
    }
}
