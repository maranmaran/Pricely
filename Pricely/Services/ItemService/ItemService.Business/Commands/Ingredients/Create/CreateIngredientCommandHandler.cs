using AutoMapper;
using Common.Exceptions;
using DataAccess.Sql.Interfaces;
using ItemService.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Commands.Ingredients.Create
{
    internal class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, Guid>
    {
        private readonly IGenericEfRepository<Ingredient> _repository;
        private readonly IMapper _mapper;

        public CreateIngredientCommandHandler(IGenericEfRepository<Ingredient> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Ingredient>(request.Ingredient);
                return await _repository.Insert(entity, cancellationToken);
            }
            catch (Exception e)
            {
                throw new CreateException(e);
            }
        }
    }
}