using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FolderFilesService.Persistence.DTOModels;
using FolderFilesService.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FolderFilesService.Business.Queries.Folder.Get
{
    internal class GetFolderQueryHandler : IRequestHandler<GetFolderQuery, FolderDto>
    {
        private readonly IRepository<Domain.Entities.Folder> _repository;
        private readonly IMapper _mapper;

        public GetFolderQueryHandler(IRepository<Domain.Entities.Folder> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FolderDto> Handle(GetFolderQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.Get(
                filter: source => source.Id == request.Id,
                include: source => source
                    .Include(x => x.Folders)
                    .Include(x => x.Files),
                cancellationToken: cancellationToken
            );

            return _mapper.Map<FolderDto>(entities);
        }
    }
}