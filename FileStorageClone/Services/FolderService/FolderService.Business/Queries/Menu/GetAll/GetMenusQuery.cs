using System.Collections.Generic;
using MediatR;
using MenuService.Persistence.DTOModels;

namespace MenuService.Business.Queries.Menu.GetAll
{
    public class GetMenusQuery : IRequest<IEnumerable<MenuDto>>
    {

    }
}
