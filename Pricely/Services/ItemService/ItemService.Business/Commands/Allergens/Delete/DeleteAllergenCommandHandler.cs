using Common.Exceptions;
using DataAccess.Sql.Interfaces;
using EventBus.Infrastructure.Interfaces;
using ItemService.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Commands.Allergens.Delete
{
    internal class DeleteAllergenCommandHandler : IRequestHandler<DeleteAllergenCommand, Unit>
    {
        private readonly IGenericEfRepository<Allergen> _repository;
        private readonly IEventBus _eventBus;

        public DeleteAllergenCommandHandler(IGenericEfRepository<Allergen> repository, IEventBus eventBus)
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