using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Exceptions;
using ItemService.Domain.Entities;
using ItemService.Persistence.Interfaces;
using MediatR;

namespace ItemService.Business.Commands.Ingredients.Create
{
    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, Guid>
    {
        private readonly IRepository<Ingredient> _repository;
        private readonly IMapper _mapper;

        public CreateIngredientCommandHandler(IRepository<Ingredient> repository, IMapper mapper)
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