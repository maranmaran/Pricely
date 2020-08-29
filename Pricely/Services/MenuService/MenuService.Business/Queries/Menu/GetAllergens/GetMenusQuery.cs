using System.Collections.Generic;
using MenuService.Persistence.DTOModels;
using MediatR;

namespace MenuService.Business.Queries.Menus.GetMenus
{
    public class GetMenusQuery : IRequest<IEnumerable<MenuDto>>
    {

    }
}
