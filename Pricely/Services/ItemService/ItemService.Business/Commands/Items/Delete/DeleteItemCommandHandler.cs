using Common.Exceptions;
using EventBus.Infrastructure.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Persistence.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Commands.Items.Delete
{
    internal class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Unit>
    {
        private readonly IRepository<Item> _repository;
        private readonly IEventBus _eventBus;
        private readonly ILogger<DeleteItemCommand> _logger;

        public DeleteItemCommandHandler(IRepository<Item> repository, IEventBus eventBus, ILogger<DeleteItemCommand> logger)
        {
            _repository = repository;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(request.Id, cancellationToken);

                _logger.LogInformation("Sending deleted item event...");
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