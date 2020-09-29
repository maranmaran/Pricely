using AutoMapper;
using DataAccess.Sql.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Queries.Allergens.GetAllergen
{
    internal class GetAllergenQueryHandler : IRequestHandler<GetAllergenQuery, AllergenDto>
    {
        private readonly IGenericEfRepository<Allergen> _repository;
        private readonly IMapper _mapper;

        public GetAllergenQueryHandler(IGenericEfRepository<Allergen> repository, IMapper mapper)
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