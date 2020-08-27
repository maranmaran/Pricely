using System;
using AutoMapper;
using EventBus.Infrastructure.Interfaces;
using EventBus.Infrastructure.Models;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;
using ItemService.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Queries.Items.GetItems
{
    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IEnumerable<ItemDto>>
    {
        private readonly IRepository<Item> _repository;
        private readonly IMapper _mapper;

        // DEMO - remove later
        private readonly IEventBus _eventBus;

        public GetItemsQueryHandler(IRepository<Item> repository, IMapper mapper, IEventBus eventBus)
        {
            _repository = repository;
            _mapper = mapper;
            _eventBus = eventBus;
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

            _eventBus.Publish(new HelloEvent()); // make new class and extend Event class

            return _mapper.Map<IEnumerable<ItemDto>>(entities);
        }
    }

    public class HelloEvent : Event
    {
        public string Title { get; set; } = "Hello world";
    }

    public class HelloEventHandler : IEventHandler<HelloEvent>
    {
        public Task Handle(HelloEvent @event)
        {
            throw new Exception($"{@event.Title}");
        }
    }

}