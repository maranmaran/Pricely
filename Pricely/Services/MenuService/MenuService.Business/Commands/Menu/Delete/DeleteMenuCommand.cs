using System;
using MediatR;

namespace MenuService.Business.Commands.Menus.Delete
{
    public class DeleteMenuCommand : IRequest<Unit>
    {
        public DeleteMenuCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
