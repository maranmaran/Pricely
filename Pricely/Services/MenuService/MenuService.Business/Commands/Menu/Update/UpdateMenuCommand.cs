using MenuService.Persistence.DTOModels;
using MediatR;

namespace MenuService.Business.Commands.Menus.Update
{
    public class UpdateMenuCommand : IRequest<Unit>
    {
        public UpdateMenuCommand(MenuDto menu)
        {
            Menu = menu;
        }

        public MenuDto Menu { get; set; }
    }
}
