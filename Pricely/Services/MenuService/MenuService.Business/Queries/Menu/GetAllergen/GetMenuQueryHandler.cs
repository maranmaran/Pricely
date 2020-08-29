using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MenuService.Domain.Entities;
using MenuService.Persistence.DTOModels;
using MenuService.Persistence.Interfaces;
using MediatR;

namespace MenuService.Business.Queries.Menus.GetMenu
{
    internal class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, MenuDto>
    {
        private readonly IRepository<Menu> _repository;
        private readonly IMapper _mapper;

        public GetMenuQueryHandler(IRepository<Menu> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MenuDto> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.Get(
                filter: source => source.Id == request.Id, cancellationToken: cancellationToken
            );

            return _mapper.Map<MenuDto>(entities);
        }
    }
}