using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using EventBus.Infrastructure.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Persistence.Interfaces;
using MediatR;

namespace ItemService.Business.Commands.Items.Delete
{
    internal class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Unit>
    {
        private readonly IRepository<Item> _repository;
        private readonly IEventBus _eventBus;

        public DeleteItemCommandHandler(IRepository<Item> repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(request.Id, cancellationToken);

                _eventBus.Publish(new ItemDeletedEvent(request.Id));

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new DeleteException(request.Id, e);
            }
        }
    }
}