using Common.Exceptions;
using DataAccess.Sql.Interfaces;
using EventBus.Infrastructure.Interfaces;
using ItemService.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Commands.Ingredients.Delete
{
    internal class DeleteIngredientCommandHandler : IRequestHandler<DeleteIngredientCommand, Unit>
    {
        private readonly IGenericEfRepository<Ingredient> _repository;
        private readonly IEventBus _eventBus;

        public DeleteIngredientCommandHandler(IGenericEfRepository<Ingredient> repository, IEventBus eventBus)
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