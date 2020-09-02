using System;
using System.Collections.Generic;
using FolderFilesService.Persistence.DTOModels;
using MediatR;

namespace FolderFilesService.Business.Queries.File.GetAll
{
    public class GetFilesQuery : IRequest<IEnumerable<FileDto>>
    {
        public Guid? ParentFolderId { get; set; }
        public string Name { get; set; }

        public GetFilesQuery(Guid? parentFolderId, string name)
        {
            ParentFolderId = parentFolderId;
            Name = name;
        }
    }
}
