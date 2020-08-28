using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Exceptions;
using EventBus.Infrastructure.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Persistence.Interfaces;
using MediatR;

namespace ItemService.Business.Commands.Allergens.Update
{
    public class UpdateAllergenCommandHandler : IRequestHandler<UpdateAllergenCommand, Unit>
    {
        private readonly IRepository<Allergen> _repository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public UpdateAllergenCommandHandler(IRepository<Allergen> repository, IMapper mapper, IEventBus eventBus)
        {
            _repository = repository;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(UpdateAllergenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Allergen>(request.Allergen);

                await _repository.Update(entity, cancellationToken);

                _eventBus.Publish(new AllergenUpdatedEvent(entity));

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new UpdateException(request.Allergen.Id, e);
            }
        }
    }
}