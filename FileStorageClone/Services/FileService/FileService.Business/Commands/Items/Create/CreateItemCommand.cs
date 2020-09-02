using System;
using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Commands.Items.Create
{
    public class CreateItemCommand : IRequest<Guid>
    {
        public CreateItemCommand(ItemDto item)
        {
            Item = item;
        }

        public ItemDto Item { get; set; }
    }
}
