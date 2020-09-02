using MediatR;
using MenuService.Persistence.DTOModels;

namespace MenuService.Business.Commands.Menu.Update
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
