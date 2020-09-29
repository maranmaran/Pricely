using AutoMapper;
using Common.Exceptions;
using DataAccess.Sql.Interfaces;
using ItemService.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Commands.Categories.Create
{
    internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IGenericEfRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(IGenericEfRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Category>(request.Category);
                return await _repository.Insert(entity, cancellationToken);
            }
            catch (Exception e)
            {
                throw new CreateException(e);
            }
        }
    }
}