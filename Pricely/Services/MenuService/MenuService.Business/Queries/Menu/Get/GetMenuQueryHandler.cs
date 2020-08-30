using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MenuService.Persistence.DTOModels;
using MenuService.Persistence.Interfaces;

namespace MenuService.Business.Queries.Menu.Get
{
    internal class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, MenuDto>
    {
        private readonly IMongoRepository<Domain.Entities.Menu> _repository;
        private readonly IMapper _mapper;

        public GetMenuQueryHandler(IMongoRepository<Domain.Entities.Menu> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MenuDto> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.FindByIdAsync(request.Id.ToString(), cancellationToken);

            return _mapper.Map<MenuDto>(entities);
        }
    }
}