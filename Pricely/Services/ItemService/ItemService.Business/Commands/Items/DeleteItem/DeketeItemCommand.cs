using MediatR;
using System;

namespace ItemService.Business.Commands.Items.DeleteItem
{
    public class DeleteItemCommand : IRequest<Unit>
    {
        public DeleteItemCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
