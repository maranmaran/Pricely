using Common.Exceptions;
using DataAccess.Sql.Interfaces;
using EventBus.Infrastructure.Interfaces;
using ItemService.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Commands.Categories.Delete
{
    internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly IGenericEfRepository<Category> _repository;
        private readonly IEventBus _eventBus;

        public DeleteCategoryCommandHandler(IGenericEfRepository<Category> repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(request.Id, cancellationToken);

                _eventBus.Publish(new CategoryDeletedEvent(request.Id));

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new DeleteException(request.Id, e);
            }
        }
    }
}