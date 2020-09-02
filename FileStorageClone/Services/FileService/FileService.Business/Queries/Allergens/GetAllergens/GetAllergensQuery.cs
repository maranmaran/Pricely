using System.Collections.Generic;
using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Queries.Allergens.GetAllergens
{
    public class GetAllergensQuery : IRequest<IEnumerable<AllergenDto>>
    {

    }
}
