using EventBus.Infrastructure.Models;
using ItemService.Domain.Entities;

namespace ItemService.Business.Commands.Categories.Update
{
    public class CategoryUpdatedEvent : Event
    {
        public CategoryUpdatedEvent(Category category)
        {
            Category = category;
        }

        public Category Category { get; set; }
    }
}
