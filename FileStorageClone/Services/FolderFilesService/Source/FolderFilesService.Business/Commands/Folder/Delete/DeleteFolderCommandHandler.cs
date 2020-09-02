using Common.Exceptions;
using FolderFilesService.Business.Queries.Folder.Get;
using FolderFilesService.Persistence.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FolderFilesService.Business.Commands.Folder.Delete
{
    internal class DeleteFolderCommandHandler : IRequestHandler<DeleteFolderCommand, Unit>
    {
        private readonly IRepository<Domain.Entities.Folder> _repository;
        private readonly IMediator _mediator;

        public DeleteFolderCommandHandler(IRepository<Domain.Entities.Folder> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await DeleteDependantChildren(request.Id, cancellationToken);

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new DeleteException(request.Id, e);
            }
        }

        /// <summary>
        /// Recursively delete all dependant children folders
        /// </summary>
        private async Task DeleteDependantChildren(Guid id, CancellationToken cancellationToken)
        {
            var folder = await _mediator.Send(new GetFolderQuery(id), cancellationToken);
            var children = folder.Folders;

            foreach (var child in children)
            {
                await DeleteDependantChildren(child.Id, cancellationToken);
            }
            
            await _repository.Delete(id, cancellationToken);
        }
    }
}