using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IdentityService.Domain.Entities;
using IdentityService.Persistence.DTOModels;
using IdentityService.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Business.Queries.Items.GetItems
{
    public class GetItemsQuery : IRequest<IEnumerable<ItemDto>>
    {

    }

    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IEnumerable<ItemDto>>
    {
        private readonly IRepository<Item> _repository;
        private readonly IMapper _mapper;

        public GetItemsQueryHandler(IRepository<Item> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAll(
                include: source => source
                    .Include(x => x.Ingredients)
                    .ThenInclude(x => x.Ingredient)
                    .Include(x => x.Allergens)
                    .ThenInclude(x => x.Allergen)
                    .Include(x => x.Category),
                cancellationToken: cancellationToken
            );

            return _mapper.Map<IEnumerable<ItemDto>>(entities);
        }
    }


}
