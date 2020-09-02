using System;
using MediatR;

namespace FolderFilesService.Business.Commands.Folder.Delete
{
    public class DeleteFolderCommand : IRequest<Unit>
    {
        public DeleteFolderCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
