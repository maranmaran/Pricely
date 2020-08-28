using System;
using ItemService.Persistence.DTOModels;
using MediatR;

namespace ItemService.Business.Queries.Ingredients.GetIngredient
{
    public class GetIngredientQuery : IRequest<IngredientDto>
    {
        public Guid Id { get; set; }

        public GetIngredientQuery(Guid id)
        {
            Id = id;
        }
    }
}
