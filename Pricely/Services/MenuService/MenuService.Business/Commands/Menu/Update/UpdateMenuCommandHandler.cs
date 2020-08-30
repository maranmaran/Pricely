using AutoMapper;
using Common.Exceptions;
using MediatR;
using MenuService.Persistence.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MenuService.Business.Commands.Menu.Update
{
    internal class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, Unit>
    {
        private readonly IMongoRepository<Domain.Entities.Menu> _repository;
        private readonly IMapper _mapper;

        public UpdateMenuCommandHandler(IMongoRepository<Domain.Entities.Menu> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Domain.Entities.Menu>(request.Menu);

                await _repository.ReplaceOneAsync(entity, cancellationToken);

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new UpdateException(request.Menu.Id, e);
            }
        }
    }
}