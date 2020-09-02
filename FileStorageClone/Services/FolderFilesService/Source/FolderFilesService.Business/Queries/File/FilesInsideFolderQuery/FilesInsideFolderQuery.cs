using FolderFilesService.Persistence.DTOModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace FolderFilesService.Business.Queries.File.GetFilesInsideFolderStructure
{
    public class FilesInsideFolderQuery : IRequest<IEnumerable<FileDto>>
    {
        public Guid? ParentFolderId { get; set; }
        public string Name { get; set; }

        public FilesInsideFolderQuery(Guid? parentFolderId, string name)
        {
            ParentFolderId = parentFolderId;
            Name = name;
        }
    }
}
