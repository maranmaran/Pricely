using AutoMapper;
using Common.Exceptions;
using DataAccess.Sql.Interfaces;
using EventBus.Infrastructure.Interfaces;
using ItemService.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Commands.Categories.Update
{
    internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly IGenericEfRepository<Category> _repository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public UpdateCategoryCommandHandler(IGenericEfRepository<Category> repository, IMapper mapper, IEventBus eventBus)
        {
            _repository = repository;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Category>(request.Category);

                await _repository.Update(entity, cancellationToken);

                _eventBus.Publish(new CategoryUpdatedEvent(request.Category));

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new UpdateException(request.Category.Id, e);
            }
        }
    }
}