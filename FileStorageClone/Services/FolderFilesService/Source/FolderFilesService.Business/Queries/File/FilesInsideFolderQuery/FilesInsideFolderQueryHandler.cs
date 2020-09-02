using AutoMapper;
using FolderFilesService.Business.Settings;
using FolderFilesService.Persistence.DTOModels;
using FolderFilesService.Persistence.Interfaces;
using LinqKit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Tests.Business")]
namespace FolderFilesService.Business.Queries.File.GetFilesInsideFolderStructure
{
    internal class FilesInsideFolderQueryHandler : IRequestHandler<FilesInsideFolderQuery, IEnumerable<FileDto>>
    {
        private readonly IRepository<Domain.Entities.File> _repository;
        private readonly IMapper _mapper;
        private readonly AppSettings _settings;

        public FilesInsideFolderQueryHandler(IRepository<Domain.Entities.File> repository, IMapper mapper, AppSettings settings)
        {
            _repository = repository;
            _mapper = mapper;
            _settings = settings;
        }

        public async Task<IEnumerable<FileDto>> Handle(FilesInsideFolderQuery request, CancellationToken cancellationToken)
        {
            var filter = GetFileFilterExpression(request);

            var entities = (await _repository.GetAll(filter, cancellationToken: cancellationToken)).Take(_settings.FilesQueryLimit);

            return _mapper.Map<IEnumerable<FileDto>>(entities);
        }

        /// <summary>
        /// Builds expression for filtering products
        /// </summary>
        internal Expression<Func<Domain.Entities.File, bool>> GetFileFilterExpression(FilesInsideFolderQuery request)
        {
            // builder filter
            var predicate = PredicateBuilder.New<Domain.Entities.File>(true);

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                predicate.And(file => file.Name.ToLower().StartsWith(request.Name.Trim().ToLower()));
            }

            if (request.ParentFolderId.HasValue)
            {
                predicate.And(file => file.ParentFolderId == request.ParentFolderId);
            }

            return predicate;
        }
    }

}