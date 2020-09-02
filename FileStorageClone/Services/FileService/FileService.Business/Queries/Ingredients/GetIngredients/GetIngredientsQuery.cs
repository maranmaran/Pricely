using System.Collections.Generic;
using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Queries.Ingredients.GetIngredients
{
    public class GetIngredientsQuery : IRequest<IEnumerable<IngredientDto>>
    {

    }
}
