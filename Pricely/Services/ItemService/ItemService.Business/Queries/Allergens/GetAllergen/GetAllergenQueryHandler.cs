using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;
using ItemService.Persistence.Interfaces;
using MediatR;

namespace ItemService.Business.Queries.Allergens.GetAllergen
{
    public class GetAllergenQueryHandler : IRequestHandler<GetAllergenQuery, AllergenDto>
    {
        private readonly IRepository<Allergen> _repository;
        private readonly IMapper _mapper;

        public GetAllergenQueryHandler(IRepository<Allergen> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AllergenDto> Handle(GetAllergenQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.Get(
                filter: source => source.Id == request.Id, cancellationToken: cancellationToken
            );

            return _mapper.Map<AllergenDto>(entities);
        }
    }
}