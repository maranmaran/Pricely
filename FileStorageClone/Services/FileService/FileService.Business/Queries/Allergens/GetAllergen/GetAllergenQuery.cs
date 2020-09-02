using System;
using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Queries.Allergens.GetAllergen
{
    public class GetAllergenQuery : IRequest<AllergenDto>
    {
        public Guid Id { get; set; }

        public GetAllergenQuery(Guid id)
        {
            Id = id;
        }
    }
}
