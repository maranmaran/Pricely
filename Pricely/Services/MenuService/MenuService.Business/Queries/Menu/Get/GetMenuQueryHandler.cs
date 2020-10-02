using AutoMapper;
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
            var entities = await _repository.FindByIdAsync(request.Id, cancellationToken);

            return _mapper.Map<MenuDto>(entities);
        }
    }
}