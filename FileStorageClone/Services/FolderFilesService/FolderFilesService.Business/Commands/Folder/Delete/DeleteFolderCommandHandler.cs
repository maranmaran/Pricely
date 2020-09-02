using Common.Exceptions;
using FolderFilesService.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FolderFilesService.Business.Commands.Folder.Delete
{
    internal class DeleteFolderCommandHandler : IRequestHandler<DeleteFolderCommand, Unit>
    {
        private readonly IRepository<Domain.Entities.Folder> _repository;

        public DeleteFolderCommandHandler(IRepository<Domain.Entities.Folder> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(
                    id: request.Id, 
                    include: folder => folder.Include(f => f.Folders).Include(f => f.Files), 
                    cancellationToken: cancellationToken);

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new DeleteException(request.Id, e);
            }
        }
    }
}