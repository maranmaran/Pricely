using System;
using MediatR;

namespace ItemService.Business.Commands.Categories.Delete
{
    public class DeleteCategoryCommand : IRequest<Unit>
    {
        public DeleteCategoryCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
