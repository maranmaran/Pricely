using EventBus.Infrastructure.Models;
using ItemService.Domain.Entities;
using ItemService.Persistence.DTOModels;

namespace ItemService.Business.Commands.Categories.Update
{
    public class CategoryUpdatedEvent : Event
    {
        public CategoryUpdatedEvent(CategoryDto category)
        {
            Category = category;
        }

        public CategoryDto Category { get; set; }
    }
}
