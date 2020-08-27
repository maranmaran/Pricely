using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;
using ItemService.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ItemService.Business.Queries.Items.GetItem
{
    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, ItemDto>
    {
        private readonly IRepository<Item> _repository;
        private readonly IMapper _mapper;

        public GetItemQueryHandler(IRepository<Item> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ItemDto> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.Get(
                filter: source => source.Id == request.Id,
                include: source => source
                    .Include(x => x.Ingredients)
                    .ThenInclude(x => x.Ingredient)
                    .Include(x => x.Allergens)
                    .ThenInclude(x => x.Allergen)
                    .Include(x => x.Category),
                cancellationToken: cancellationToken
            );

            return _mapper.Map<ItemDto>(entities);
        }
    }
}