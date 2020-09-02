using System;
using MediatR;

namespace ItemService.Business.Commands.Items.Delete
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
