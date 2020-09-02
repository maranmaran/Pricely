using System;
using System.Collections.Generic;
using FolderFilesService.Persistence.DTOModels;
using MediatR;

namespace FolderFilesService.Business.Queries.File.GetFolderFilesRecusivelyQuery
{
    public class GetFolderFilesRecusivelyQuery : IRequest<IEnumerable<FileDto>>
    {
        public Guid? ParentFolderId { get; set; }
        public string Name { get; set; }

        public GetFolderFilesRecusivelyQuery(Guid? parentFolderId, string name)
        {
            ParentFolderId = parentFolderId;
            Name = name;
        }
    }
}
