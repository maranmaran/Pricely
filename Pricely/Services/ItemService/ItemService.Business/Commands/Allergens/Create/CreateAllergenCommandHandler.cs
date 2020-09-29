using AutoMapper;
using Common.Exceptions;
using DataAccess.Sql.Interfaces;
using ItemService.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Commands.Allergens.Create
{
    internal class CreateAllergenCommandHandler : IRequestHandler<CreateAllergenCommand, Guid>
    {
        private readonly IGenericEfRepository<Allergen> _repository;
        private readonly IMapper _mapper;

        public CreateAllergenCommandHandler(IGenericEfRepository<Allergen> repository, IMapper mapper)
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