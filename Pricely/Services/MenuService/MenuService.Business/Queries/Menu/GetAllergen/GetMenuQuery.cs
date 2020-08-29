using System;
using MenuService.Persistence.DTOModels;
using MediatR;

namespace MenuService.Business.Queries.Menus.GetMenu
{
    public class GetMenuQuery : IRequest<MenuDto>
    {
        public Guid Id { get; set; }

        public GetMenuQuery(Guid id)
        {
            Id = id;
        }
    }
}
