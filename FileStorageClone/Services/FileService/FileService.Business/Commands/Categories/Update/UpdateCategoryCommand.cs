using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Commands.Categories.Update
{
    public class UpdateCategoryCommand : IRequest<Unit>
    {
        public UpdateCategoryCommand(CategoryDto category)
        {
            Category = category;
        }

        public CategoryDto Category { get; set; }
    }
}
