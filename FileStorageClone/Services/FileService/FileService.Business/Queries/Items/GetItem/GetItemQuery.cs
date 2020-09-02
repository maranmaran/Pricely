using System;
using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Queries.Items.GetItem
{
    public class GetItemQuery : IRequest<ItemDto>
    {
        public Guid Id { get; set; }

        public GetItemQuery(Guid id)
        {
            Id = id;
        }
    }
}
