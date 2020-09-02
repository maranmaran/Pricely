using AutoMapper;
using FolderFilesService.Persistence.DTOModels;
using FolderFilesService.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests.Business")]
namespace FolderFilesService.Business.Queries.Folder.GetContents
{
    internal class GetFolderContentsQueryHandler : IRequestHandler<GetFolderContentsQuery, IEnumerable<FolderDto>>
    {
        private readonly IRepository<Domain.Entities.Folder> _repository;
        private readonly IMapper _mapper;

        public GetFolderContentsQueryHandler(IRepository<Domain.Entities.Folder> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FolderDto>> Handle(GetFolderContentsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAll(
                filter: file => file.ParentFolderId == request.Id,
                include: source => source
                    .Include(x => x.Folders)
                    .Include(x => x.Files),
                cancellationToken: cancellationToken
            );

            return _mapper.Map<IEnumerable<FolderDto>>(entities);
        }

    }

}