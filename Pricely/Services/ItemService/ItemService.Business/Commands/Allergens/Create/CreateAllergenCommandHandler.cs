using AutoMapper;
using Common.Exceptions;
using ItemService.Domain.Entities;
using ItemService.Persistence.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Commands.Allergens.Create
{
    public class CreateAllergenCommandHandler : IRequestHandler<CreateAllergenCommand, Guid>
    {
        private readonly IRepository<Allergen> _repository;
        private readonly IMapper _mapper;

        public CreateAllergenCommandHandler(IRepository<Allergen> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateAllergenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Allergen>(request.Allergen);
                throw new Exception();
                return await _repository.Insert(entity, cancellationToken);
            }
            catch (Exception e)
            {
                throw new CreateException(e);
            }
        }
    }
}