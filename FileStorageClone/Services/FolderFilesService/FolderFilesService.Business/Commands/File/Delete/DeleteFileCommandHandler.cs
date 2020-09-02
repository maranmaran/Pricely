﻿using Common.Exceptions;
using FolderFilesService.Persistence.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FolderFilesService.Business.Commands.File.Delete
{
    internal class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, Unit>
    {
        private readonly IRepository<Domain.Entities.File> _repository;

        public DeleteFileCommandHandler(IRepository<Domain.Entities.File> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(request.Id, cancellationToken: cancellationToken);

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new DeleteException(request.Id, e);
            }
        }
    }
}