using FolderFilesService.Persistence.DTOModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace FolderFilesService.Business.Queries.File.GetFolderFiles
{
    public class GetFolderFilesQuery: IRequest<IEnumerable<FileDto>>
    {
        public GetFolderFilesQuery(Guid? parentFolderId)
        {
            ParentFolderId = parentFolderId;
        }

        public Guid? ParentFolderId { get; set; }
    }
}
