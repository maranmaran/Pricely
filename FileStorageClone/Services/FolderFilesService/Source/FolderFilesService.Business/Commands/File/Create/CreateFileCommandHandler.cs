using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Exceptions;
using FolderFilesService.Persistence.Interfaces;
using MediatR;

[assembly: InternalsVisibleTo("Tests.Business")]
namespace FolderFilesService.Business.Commands.File.Create
{
    internal class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, Guid>
    {
        private readonly IRepository<Domain.Entities.File> _repository;
        private readonly IMapper _mapper;

        public CreateFileCommandHandler(IRepository<Domain.Entities.File> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Domain.Entities.File>(request);
                return await _repository.Insert(entity, cancellationToken);
            }
            catch (Exception e)
            {
                throw new CreateException(e);
            }
        }
    }
}