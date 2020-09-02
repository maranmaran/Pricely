using System;
using MediatR;

namespace FolderFilesService.Business.Commands.File.Delete
{
    public class DeleteFileCommand : IRequest<Unit>
    {
        public DeleteFileCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
