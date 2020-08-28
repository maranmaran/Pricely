using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using EventBus.Infrastructure.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Persistence.Interfaces;
using MediatR;

namespace ItemService.Business.Commands.Allergens.Delete
{
    public class DeleteAllergenCommandHandler : IRequestHandler<DeleteAllergenCommand, Unit>
    {
        private readonly IRepository<Allergen> _repository;
        private readonly IEventBus _eventBus;

        public DeleteAllergenCommandHandler(IRepository<Allergen> repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(DeleteAllergenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(request.Id, cancellationToken);

                _eventBus.Publish(new AllergenDeletedEvent(request.Id));

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new DeleteException(request.Id, e);
            }
        }
    }
}