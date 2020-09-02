using System;
using System.Collections.Generic;
using FolderFilesService.Persistence.DTOModels;
using MediatR;

namespace FolderFilesService.Business.Queries.Folder.GetContents
{
    public class GetFolderContentsQuery : IRequest<IEnumerable<FolderDto>>
    {
        public Guid? Id { get; set; }

        public GetFolderContentsQuery(Guid? id)
        {
            Id = id;
        }
    }
}
