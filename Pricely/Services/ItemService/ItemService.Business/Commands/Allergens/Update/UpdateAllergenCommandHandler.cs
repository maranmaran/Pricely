using AutoMapper;
using Common.Exceptions;
using DataAccess.Sql.Interfaces;
using EventBus.Infrastructure.Interfaces;
using ItemService.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Commands.Allergens.Update
{
    internal class UpdateAllergenCommandHandler : IRequestHandler<UpdateAllergenCommand, Unit>
    {
        private readonly IGenericEfRepository<Allergen> _repository;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;

        public UpdateAllergenCommandHandler(IGenericEfRepository<Allergen> repository, IMapper mapper, IEventBus eventBus)
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

                _eventBus.Publish(new AllergenUpdatedEvent(request.Allergen));

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new UpdateException(request.Allergen.Id, e);
            }
        }
    }
}