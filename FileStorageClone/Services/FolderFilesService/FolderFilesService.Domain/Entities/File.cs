using System;

namespace FolderFilesService.Domain.Entities
{
    public class File : EntityBase
    {
        public string Name { get; set; }

        public Guid? ParentFolderId { get; set; }
        public virtual Folder ParentFolder { get; set; }
    }
}