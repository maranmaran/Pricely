using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;
using ItemService.Persistence.Interfaces;
using MediatR;

namespace ItemService.Business.Queries.Categories.GetCategories
{
    internal class GetCategorysQueryHandler : IRequestHandler<GetCategorysQuery, IEnumerable<CategoryDto>>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategorysQueryHandler(IRepository<Category> repository, IMapper mapper)
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