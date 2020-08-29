using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MenuService.Domain.Entities;
using MenuService.Persistence.DTOModels;
using MenuService.Persistence.Interfaces;
using MediatR;

namespace MenuService.Business.Queries.Menus.GetMenus
{
    internal class GetMenusQueryHandler : IRequestHandler<GetMenusQuery, IEnumerable<MenuDto>>
    {
        private readonly IRepository<Menu> _repository;
        private readonly IMapper _mapper;

        public GetMenusQueryHandler(IRepository<Menu> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuDto>> Handle(GetMenusQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAll(cancellationToken: cancellationToken);

            return _mapper.Map<IEnumerable<MenuDto>>(entities);
        }
    }

}