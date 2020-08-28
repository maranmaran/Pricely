using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using EventBus.Infrastructure.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Persistence.Interfaces;
using MediatR;

namespace ItemService.Business.Commands.Ingredients.Delete
{
    internal class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand, Unit>
    {
        private readonly IRepository<Ingredient> _repository;
        private readonly IEventBus _eventBus;

        public DeleteIngredientCommandHandler(IRepository<Ingredient> repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(request.Id, cancellationToken);

                _eventBus.Publish(new IngredientDeletedEvent(request.Id));

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new DeleteException(request.Id, e);
            }
        }
    }
}