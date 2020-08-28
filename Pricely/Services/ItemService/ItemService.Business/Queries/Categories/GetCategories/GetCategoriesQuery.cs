using System.Collections.Generic;
using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Queries.Categories.GetCategories
{
    public class GetCategorysQuery : IRequest<IEnumerable<CategoryDto>>
    {

    }
}
