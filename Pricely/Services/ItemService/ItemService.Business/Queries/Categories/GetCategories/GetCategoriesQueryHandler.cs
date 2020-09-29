using AutoMapper;
using DataAccess.Sql.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Queries.Categories.GetCategories
{
    internal class GetCategoriesQueryHandler : IRequestHandler<GetCategorysQuery, IEnumerable<CategoryDto>>
    {
        private readonly IGenericEfRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IGenericEfRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetCategorysQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAll(cancellationToken: cancellationToken);

            return _mapper.Map<IEnumerable<CategoryDto>>(entities);
        }
    }

}