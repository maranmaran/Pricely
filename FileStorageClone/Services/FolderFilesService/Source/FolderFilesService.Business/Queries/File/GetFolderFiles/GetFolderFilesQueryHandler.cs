﻿using AutoMapper;
using FolderFilesService.Persistence.DTOModels;
using FolderFilesService.Persistence.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FolderFilesService.Business.Queries.File.GetFolderFiles
{
    internal class GetFolderFilesQueryHandler : IRequestHandler<GetFolderFilesQuery, IEnumerable<FileDto>>
    {
        private readonly IRepository<Domain.Entities.File> _repository;
        private readonly IMapper _mapper;

        public GetFolderFilesQueryHandler(IRepository<Domain.Entities.File> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FileDto>> Handle(GetFolderFilesQuery request, CancellationToken cancellationToken)
        {
            var entities = (await _repository.GetAll(
                file => file.ParentFolderId == request.ParentFolderId, 
                cancellationToken: cancellationToken));

            return _mapper.Map<IEnumerable<FileDto>>(entities);
        }

    }
}