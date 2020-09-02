using System;
using FolderFilesService.Persistence.DTOModels;
using MediatR;

namespace FolderFilesService.Business.Commands.File.Create
{
    public class CreateFileCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Guid? ParentFolderId { get; set; }
    }
}
