using Common.Exceptions;
using EventBus.Infrastructure.Interfaces;
using MediatR;
using MenuService.Domain.Entities;
using MenuService.Persistence.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MenuService.Business.Commands.Menus.Delete
{
    internal class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, Unit>
    {
        private readonly IRepository<Menu> _repository;
        private readonly IEventBus _eventBus;

        public DeleteMenuCommandHandler(IRepository<Menu> repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(request.Id, cancellationToken);

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new DeleteException(request.Id, e);
            }
        }
    }
}