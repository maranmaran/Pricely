using AutoMapper;
using FolderFilesService.Persistence.DTOModels;
using FolderFilesService.Persistence.Interfaces;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests.Business")]
namespace FolderFilesService.Business.Queries.Folder.GetContents
{
    internal class GetFolderContentsQueryHandler : IRequestHandler<GetFolderContentsQuery, IEnumerable<FolderDto>>
    {
        private readonly IRepository<Domain.Entities.Folder> _repository;
        private readonly IRepository<Domain.Entities.File> _fileRepository;
        private readonly IMapper _mapper;

        public GetFolderContentsQueryHandler(IRepository<Domain.Entities.Folder> repository, IMapper mapper, IRepository<Domain.Entities.File> fileRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public async Task<IEnumerable<FolderDto>> Handle(GetFolderContentsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAll(
                filter: GetFolderFilterExpression(request),
                include: source => source
                    .Include(x => x.Folders)
                    .Include(x => x.Files),
                cancellationToken: cancellationToken
            );

            return _mapper.Map<IEnumerable<FolderDto>>(entities);
        }

        /// <summary>
        /// Builds expression for filtering folders
        /// </summary>
        internal Expression<Func<Domain.Entities.Folder, bool>> GetFolderFilterExpression(GetFolderContentsQuery request)
        {
            var predicate = PredicateBuilder.New<Domain.Entities.Folder>(true);

            if (request.Id.HasValue)
            {
                predicate.And(file => file.Id == request.Id);
            }
            else
            {
                predicate.And(file => file.ParentFolderId == null);
            }

            return predicate;
        }
    }

}