using System;
using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Queries.Categories.GetCategory
{
    public class GetCategoryQuery : IRequest<CategoryDto>
    {
        public Guid Id { get; set; }

        public GetCategoryQuery(Guid id)
        {
            Id = id;
        }
    }
}
