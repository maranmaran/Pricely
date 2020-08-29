using MediatR;
using MenuService.Persistence.DTOModels;
using System;

namespace MenuService.Business.Commands.Menus.Create
{
    public class CreateMenuCommand : IRequest<Guid>
    {
        public CreateMenuCommand(MenuDto menu)
        {
            Menu = menu;
        }

        public MenuDto Menu { get; set; }
    }
}
