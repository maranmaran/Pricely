using System;
using MediatR;

namespace ItemService.Business.Commands.Ingredients.Delete
{
    public class DeleteIngredientCommand : IRequest<Unit>
    {
        public DeleteIngredientCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
