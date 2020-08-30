using System;
using MediatR;
using MenuService.Persistence.DTOModels;

namespace MenuService.Business.Queries.Menu.Get
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
