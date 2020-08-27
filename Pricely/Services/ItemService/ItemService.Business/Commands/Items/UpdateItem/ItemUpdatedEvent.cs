﻿using EventBus.Infrastructure.Models;
using ItemService.Domain.Entities;

namespace ItemService.Business.Commands.Items.UpdateItem
{
    public class ItemUpdatedEvent : Event
    {
        public ItemUpdatedEvent(Item item)
        {
            Item = item;
        }

        public Item Item { get; set; }
    }
}
