using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Commands.Items.UpdateItem
{
    public class UpdateItemCommand : IRequest<Unit>
    {
        public UpdateItemCommand(ItemDto item)
        {
            Item = item;
        }

        public ItemDto Item { get; set; }
    }
}
