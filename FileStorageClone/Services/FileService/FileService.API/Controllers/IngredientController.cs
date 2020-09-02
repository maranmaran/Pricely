using System;
using System.Threading;
using System.Threading.Tasks;
using ItemService.Business.Commands.Ingredients.Create;
using ItemService.Business.Commands.Ingredients.Delete;
using ItemService.Business.Commands.Ingredients.Update;
using ItemService.Business.Queries.Ingredients.GetIngredient;
using ItemService.Business.Queries.Ingredients.GetIngredients;
using ItemService.Persistence.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace ItemService.API.Controllers
{
    public class IngredientController : BaseController
    {

        /// <summary>
        /// Retrieves item
        /// </summary>
        /// <remarks>
        /// Retrieves single item
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetIngredientQuery(id), cancellationToken));
        }

        /// <summary>
        /// Retrieves items
        /// </summary>
        /// <remarks>
        /// Retrieves all items if no query parameters are specified
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetIngredientsQuery(), cancellationToken));
        }

        /// <summary>
        /// Creates item
        /// </summary>
        /// <remarks>
        /// Creates item that can be placed on menu
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IngredientDto item, CancellationToken cancellationToken = default)
        {
            // TODO: Refactor this to created at..
            return Ok(await Mediator.Send(new CreateIngredientCommand(item), cancellationToken));
        }

        /// <summary>
        /// Updates item
        /// </summary>
        /// <remarks>
        /// Updates item that can be placed on menu
        /// Publishes event that item has been changed
        /// </remarks>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] IngredientDto item, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new UpdateIngredientCommand(item), cancellationToken));
        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <remarks>
        /// Deletes item
        /// Publishes event that item has been changed
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new DeleteIngredientCommand(id), cancellationToken));
        }
    }
}