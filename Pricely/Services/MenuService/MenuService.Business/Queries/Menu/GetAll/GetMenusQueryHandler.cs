using AutoMapper;
using MediatR;
using MenuService.Persistence.DTOModels;
using MenuService.Persistence.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MenuService.Business.Queries.Menu.GetAll
{
    internal class GetMenusQueryHandler : IRequestHandler<GetMenusQuery, IEnumerable<MenuDto>>
    {
        private readonly IMongoRepository<Domain.Entities.Menu> _repository;
        private readonly IMapper _mapper;

        public GetMenusQueryHandler(IMongoRepository<Domain.Entities.Menu> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuDto>> Handle(GetMenusQuery request, CancellationToken cancellationToken)
        {
            //var entities = _repository.FilterBy(x => ); // Query by user
            var entities = await _repository.AsQueryable().ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<MenuDto>>(entities);
        }
    }

}