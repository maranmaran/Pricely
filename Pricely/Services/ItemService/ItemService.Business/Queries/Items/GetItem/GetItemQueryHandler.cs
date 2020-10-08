using AutoMapper;
using DataAccess.Sql.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Queries.Items.GetItem
{
    internal class GetItemQueryHandler : IRequestHandler<GetItemQuery, ItemDto>
    {
        private readonly IGenericEfRepository<Item, ItemDto> _repository;
        private readonly IMapper _mapper;

        public GetItemQueryHandler(IGenericEfRepository<Item, ItemDto> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ItemDto> Handle(GetItemQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.Get(
                filter: source => source.Id == request.Id,
                include: source => source
                    .Include(x => x.Ingredients)
                    .ThenInclude(x => x.Ingredient)
                    .Include(x => x.Allergens)
                    .ThenInclude(x => x.Allergen)
                    .Include(x => x.Category),
                cancellationToken: cancellationToken
            );

            return entity;
        }
    }
}