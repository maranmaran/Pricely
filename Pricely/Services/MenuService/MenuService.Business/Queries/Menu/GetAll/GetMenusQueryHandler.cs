using AutoMapper;
using DataAccess.NoSql.Interfaces;
using MediatR;
using MenuService.Persistence.DTOModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MenuService.Business.Queries.Menu.GetAll
{
    internal class GetMenusQueryHandler : IRequestHandler<GetMenusQuery, IEnumerable<MenuDto>>
    {
        private readonly IGenericDocumentRepository<Domain.Entities.Menu> _repository;
        private readonly IMapper _mapper;

        public GetMenusQueryHandler(IGenericDocumentRepository<Domain.Entities.Menu> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuDto>> Handle(GetMenusQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAll(cancellationToken);

            return _mapper.Map<IEnumerable<MenuDto>>(entities);
        }
    }

}