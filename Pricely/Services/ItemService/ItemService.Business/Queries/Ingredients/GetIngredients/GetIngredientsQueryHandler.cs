using AutoMapper;
using DataAccess.Sql.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Queries.Ingredients.GetIngredients
{
    internal class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, IEnumerable<IngredientDto>>
    {
        private readonly IGenericEfRepository<Ingredient> _repository;
        private readonly IMapper _mapper;

        public GetIngredientsQueryHandler(IGenericEfRepository<Ingredient> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IngredientDto>> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAll(cancellationToken: cancellationToken);

            return _mapper.Map<IEnumerable<IngredientDto>>(entities);
        }
    }

}