using AutoMapper;
using Common.Exceptions;
using EventBus.Infrastructure.Interfaces;
using MediatR;
using MenuService.Domain.Entities;
using MenuService.Persistence.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MenuService.Business.Commands.Menus.Update
{
    internal class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, Unit>
    {
        private readonly IRepository<Menu> _repository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public UpdateMenuCommandHandler(IRepository<Menu> repository, IMapper mapper, IEventBus eventBus)
        {
            _repository = repository;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Menu>(request.Menu);

                await _repository.Update(entity, cancellationToken);

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new UpdateException(request.Menu.Id, e);
            }
        }
    }
}