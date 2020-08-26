using System;
using IdentityService.Persistence.DTOModels;
using MediatR;

namespace IdentityService.Business.Commands.Items.CreateItem
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
