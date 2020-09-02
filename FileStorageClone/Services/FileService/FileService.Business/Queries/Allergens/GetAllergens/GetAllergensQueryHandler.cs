using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;
using ItemService.Persistence.Interfaces;
using MediatR;

namespace ItemService.Business.Queries.Allergens.GetAllergens
{
    internal class GetAllergensQueryHandler : IRequestHandler<GetAllergensQuery, IEnumerable<AllergenDto>>
    {
        private readonly IRepository<Allergen> _repository;
        private readonly IMapper _mapper;

        public GetAllergensQueryHandler(IRepository<Allergen> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AllergenDto>> Handle(GetAllergensQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAll(cancellationToken: cancellationToken);

            return _mapper.Map<IEnumerable<AllergenDto>>(entities);
        }
    }

}