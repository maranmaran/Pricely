using System;
using FolderFilesService.Persistence.DTOModels;
using MediatR;

namespace FolderFilesService.Business.Commands.Folder.Create
{
    public class CreateFolderCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Guid? ParentFolderId { get; set; }

    }
}
