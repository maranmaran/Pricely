using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FolderFilesService.Persistence.DTOModels;
using FolderFilesService.Persistence.Interfaces;
using LinqKit;
using MediatR;

namespace FolderFilesService.Business.Queries.File.GetAll
{
    internal class GetFilesQueryHandler : IRequestHandler<GetFilesQuery, IEnumerable<FileDto>>
    {
        private readonly IRepository<Domain.Entities.File> _repository;
        private readonly IMapper _mapper;

        public GetFilesQueryHandler(IRepository<Domain.Entities.File> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FileDto>> Handle(GetFilesQuery request, CancellationToken cancellationToken)
        {
            var filter = GetFileFilterExpression(request);

            // top 10 filtered files as per business requirements
            // TODO: outsource this magic number to some config setting for modifying
            var entities = (await _repository.GetAll(filter, cancellationToken: cancellationToken)).Take(10);

            return _mapper.Map<IEnumerable<FileDto>>(entities);
        }

        /// <summary>
        /// Builds expression for filtering products
        /// </summary>
        internal Expression<Func<Domain.Entities.File, bool>> GetFileFilterExpression(GetFilesQuery request)
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