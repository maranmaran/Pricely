using AutoMapper;
using DataAccess.Sql.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Queries.Ingredients.GetIngredient
{
    internal class GetIngredientQueryHandler : IRequestHandler<GetIngredientQuery, IngredientDto>
    {
        private readonly IGenericEfRepository<Ingredient> _repository;
        private readonly IMapper _mapper;

        public GetIngredientQueryHandler(IGenericEfRepository<Ingredient> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IngredientDto> Handle(GetIngredientQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.Get(
                filter: source => source.Id == request.Id, cancellationToken: cancellationToken
            );

            return _mapper.Map<IngredientDto>(entities);
        }
    }
}