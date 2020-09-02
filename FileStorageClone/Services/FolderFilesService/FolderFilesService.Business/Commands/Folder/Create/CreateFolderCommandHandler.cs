﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Exceptions;
using FolderFilesService.Persistence.Interfaces;
using MediatR;

namespace FolderFilesService.Business.Commands.Folder.Create
{
    internal class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, Guid>
    {
        private readonly IRepository<Domain.Entities.Folder> _repository;
        private readonly IMapper _mapper;

        public CreateFolderCommandHandler(IRepository<Domain.Entities.Folder> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Domain.Entities.Folder>(request);
                return await _repository.Insert(entity, cancellationToken);
            }
            catch (Exception e)
            {
                throw new CreateException(e);
            }
        }
    }
}