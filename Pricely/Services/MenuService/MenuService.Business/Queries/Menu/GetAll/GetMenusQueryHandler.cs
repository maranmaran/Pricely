using AutoMapper;
using DataAccess.NoSql.Interfaces;
using MediatR;
using MenuService.Persistence.DTOModels;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
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
            //var entities = _repository.FilterBy(x => ); // Query by user
            var entities = await ((IMongoQueryable<Domain.Entities.Menu>)_repository.AsQueryable()).ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<MenuDto>>(entities);
        }
    }

}