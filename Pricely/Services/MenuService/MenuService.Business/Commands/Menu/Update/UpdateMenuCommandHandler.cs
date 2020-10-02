using AutoMapper;
using Common.Exceptions;
using DataAccess.NoSql.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MenuService.Business.Commands.Menu.Update
{
    internal class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, Unit>
    {
        private readonly IGenericDocumentRepository<Domain.Entities.Menu> _repository;
        private readonly IMapper _mapper;

        public UpdateMenuCommandHandler(IGenericDocumentRepository<Domain.Entities.Menu> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Domain.Entities.Menu>(request.Menu);

                await _repository.UpdateOneAsync(entity, cancellationToken);

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new UpdateException(request.Menu.Id, e);
            }
        }
    }
}