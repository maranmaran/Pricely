using System;
using FolderFilesService.Persistence.DTOModels;
using MediatR;

namespace FolderFilesService.Business.Queries.Folder.Get
{
    public class GetFolderQuery : IRequest<FolderDto>
    {
        public Guid Id { get; set; }

        public GetFolderQuery(Guid id)
        {
            Id = id;
        }
    }
}
