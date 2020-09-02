using System;
using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Commands.Categories.Create
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public CreateCategoryCommand(CategoryDto category)
        {
            Category = category;
        }

        public CategoryDto Category { get; set; }
    }
}
