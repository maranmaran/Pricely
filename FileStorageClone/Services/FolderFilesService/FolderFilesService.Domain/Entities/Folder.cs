using System;
using System.Collections.Generic;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace FolderFilesService.Domain.Entities
{
    public class Folder : EntityBase
    {
        public string Name { get; set; }

        public Guid? ParentFolderId { get; set; }
        public virtual Folder ParentFolder { get; set; }

        public virtual ICollection<Folder> Folders { get; set; } = new HashSet<Folder>();
        public virtual ICollection<File> Files { get; set; } = new HashSet<File>();
    }
}
