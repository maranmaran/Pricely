using Common.Exceptions;
using DataAccess.NoSql.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MenuService.Business.Commands.Menu.Delete
{
    internal class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, Unit>
    {
        private readonly IGenericDocumentRepository<Domain.Entities.Menu> _repository;

        public DeleteMenuCommandHandler(IGenericDocumentRepository<Domain.Entities.Menu> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeleteByIdAsync(request.Id, cancellationToken);

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new DeleteException(request.Id, e);
            }
        }
    }
}