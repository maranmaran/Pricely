using ItemService.Persistence.DTOModels;
using MediatR;
using System.Collections.Generic;

namespace ItemService.Business.Queries.Items.GetItems
{
    public class GetItemsQuery : IRequest<IEnumerable<ItemDto>>
    {

    }
}
