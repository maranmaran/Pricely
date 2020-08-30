using System;
using MediatR;
using MenuService.Persistence.DTOModels;

namespace MenuService.Business.Commands.Menu.Create
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
