using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Exceptions;
using ItemService.Domain.Entities;
using ItemService.Persistence.Interfaces;
using MediatR;

namespace ItemService.Business.Commands.Items.Create
{
    internal class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Guid>
    {
        private readonly IRepository<Item> _repository;
        private readonly IMapper _mapper;

        public CreateItemCommandHandler(IRepository<Item> repository, IMapper mapper)
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