using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FolderFilesService.Persistence.DTOModels;
using FolderFilesService.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FolderFilesService.Business.Queries.Folder.GetAll
{
    internal class GetFoldersQueryHandler : IRequestHandler<GetFoldersQuery, IEnumerable<FolderDto>>
    {
        private readonly IRepository<Domain.Entities.Folder> _repository;
        private readonly IMapper _mapper;

        public GetFoldersQueryHandler(IRepository<Domain.Entities.Folder> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FolderDto>> Handle(GetFoldersQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAll(
                include: source => source
                    .Include(x => x.Folders)
                    .Include(x => x.Files),
                cancellationToken: cancellationToken
            );

            return _mapper.Map<IEnumerable<FolderDto>>(entities);
        }
    }

}