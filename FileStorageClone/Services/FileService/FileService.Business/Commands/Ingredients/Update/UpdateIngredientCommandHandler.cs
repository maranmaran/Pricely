using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Exceptions;
using EventBus.Infrastructure.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Persistence.Interfaces;
using MediatR;

namespace ItemService.Business.Commands.Ingredients.Update
{
    internal class UpdateIngredientCommandHandler : IRequestHandler<UpdateIngredientCommand, Unit>
    {
        private readonly IRepository<Ingredient> _repository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public UpdateIngredientCommandHandler(IRepository<Ingredient> repository, IMapper mapper, IEventBus eventBus)
        {
            _repository = repository;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Ingredient>(request.Ingredient);

                await _repository.Update(entity, cancellationToken);

                _eventBus.Publish(new IngredientUpdatedEvent(request.Ingredient));

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new UpdateException(request.Ingredient.Id, e);
            }
        }
    }
}