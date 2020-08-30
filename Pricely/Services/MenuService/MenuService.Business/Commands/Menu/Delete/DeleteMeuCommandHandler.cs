using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using EventBus.Infrastructure.Interfaces;
using MediatR;
using MenuService.Persistence.Interfaces;

namespace MenuService.Business.Commands.Menu.Delete
{
    internal class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, Unit>
    {
        private readonly IMongoRepository<Domain.Entities.Menu> _repository;
        private readonly IEventBus _eventBus;

        public DeleteMenuCommandHandler(IMongoRepository<Domain.Entities.Menu> repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeleteByIdAsync(request.Id.ToString(), cancellationToken);

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new DeleteException(request.Id, e);
            }
        }
    }
}