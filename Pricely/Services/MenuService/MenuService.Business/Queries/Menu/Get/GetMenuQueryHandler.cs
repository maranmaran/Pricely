using AutoMapper;
using Common.Exceptions;
using DataAccess.NoSql.Interfaces;
using MediatR;
using MenuService.Persistence.DTOModels;
using System.Threading;
using System.Threading.Tasks;

namespace MenuService.Business.Queries.Menu.Get
{
    internal class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, MenuDto>
    {
        private readonly IGenericDocumentRepository<Domain.Entities.Menu> _repository;
        private readonly IMapper _mapper;

        public GetMenuQueryHandler(IGenericDocumentRepository<Domain.Entities.Menu> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MenuDto> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.FindByIdAsync(request.Id, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(Menu), $"No menu found with id: {request.Id}");

            return _mapper.Map<MenuDto>(entity);
        }
    }
}