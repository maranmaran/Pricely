using AutoMapper;
using Common.Exceptions;
using MenuService.Domain.Entities;
using MenuService.Persistence.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MenuService.Business.Commands.Menus.Create
{
    internal class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, Guid>
    {
        private readonly IRepository<Menu> _repository;
        private readonly IMapper _mapper;

        public CreateMenuCommandHandler(IRepository<Menu> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Menu>(request.Menu);
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