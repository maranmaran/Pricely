using ItemService.Persistence.DTOModels;
using MediatR;
using System;

namespace ItemService.Business.Commands.Items.CreateItem
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
