using System;
using MediatR;

namespace ItemService.Business.Commands.Allergens.Delete
{
    public class DeleteAllergenCommand : IRequest<Unit>
    {
        public DeleteAllergenCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
