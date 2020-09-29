using AutoMapper;
using DataAccess.Sql.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Queries.Categories.GetCategory
{
    internal class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryDto>
    {
        private readonly IGenericEfRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IGenericEfRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.Get(
                filter: source => source.Id == request.Id, cancellationToken: cancellationToken
            );

            return _mapper.Map<CategoryDto>(entities);
        }
    }
}