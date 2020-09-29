using AutoMapper;
using Common.Exceptions;
using DataAccess.Sql.Interfaces;
using ItemService.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.Business.Commands.Items.Create
{
    internal class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Guid>
    {
        private readonly IGenericEfRepository<Item> _repository;
        private readonly IMapper _mapper;

        public CreateItemCommandHandler(IGenericEfRepository<Item> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Item>(request.Item);
                return await _repository.Insert(entity, cancellationToken);
            }
            catch (Exception e)
            {
                throw new CreateException(e);
            }
        }
    }
}