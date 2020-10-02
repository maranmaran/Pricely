using AutoMapper;
using Common.Exceptions;
using DataAccess.NoSql.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MenuService.Business.Commands.Menu.Create
{
    internal class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, Guid>
    {
        private readonly IGenericDocumentRepository<Domain.Entities.Menu> _repository;
        private readonly IMapper _mapper;

        public CreateMenuCommandHandler(IGenericDocumentRepository<Domain.Entities.Menu> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Domain.Entities.Menu>(request.Menu);
                return await _repository.InsertOneAsync(entity, cancellationToken);
            }
            catch (Exception e)
            {
                throw new CreateException(e);
            }
        }
    }
}