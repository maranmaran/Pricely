using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;
using ItemService.Persistence.Interfaces;
using MediatR;

namespace ItemService.Business.Queries.Ingredients.GetIngredient
{
    public class GetIngredientQueryHandler : IRequestHandler<GetIngredientQuery, IngredientDto>
    {
        private readonly IRepository<Ingredient> _repository;
        private readonly IMapper _mapper;

        public GetIngredientQueryHandler(IRepository<Ingredient> repository, IMapper mapper)
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