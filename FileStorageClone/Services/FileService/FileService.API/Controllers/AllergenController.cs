using System;
using System.Threading;
using System.Threading.Tasks;
using ItemService.Business.Commands.Allergens.Create;
using ItemService.Business.Commands.Allergens.Delete;
using ItemService.Business.Commands.Allergens.Update;
using ItemService.Business.Queries.Allergens.GetAllergen;
using ItemService.Business.Queries.Allergens.GetAllergens;
using ItemService.Persistence.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace ItemService.API.Controllers
{
    public class AllergenController : BaseController
    {

        /// <summary>
        /// Retrieves allergen
        /// </summary>
        /// <remarks>
        /// Retrieves single allergen
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetAllergenQuery(id), cancellationToken));
        }

        /// <summary>
        /// Retrieves allergens
        /// </summary>
        /// <remarks>
        /// Retrieves all allergens if no query parameters are specified
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetAllergensQuery(), cancellationToken));
        }

        /// <summary>
        /// Creates allergen
        /// </summary>
        /// <remarks>
        /// Creates allergen that can be placed on menu
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AllergenDto allergen, CancellationToken cancellationToken = default)
        {
            // TODO: Refactor this to created at..
            return Ok(await Mediator.Send(new CreateAllergenCommand(allergen), cancellationToken));
        }

        /// <summary>
        /// Updates allergen
        /// </summary>
        /// <remarks>
        /// Updates allergen that can be placed on menu
        /// Publishes event that allergen has been changed
        /// </remarks>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AllergenDto allergen, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new UpdateAllergenCommand(allergen), cancellationToken));
        }

        /// <summary>
        /// Delete allergen
        /// </summary>
        /// <remarks>
        /// Deletes allergen
        /// Publishes event that allergen has been changed
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new DeleteAllergenCommand(id), cancellationToken));
        }
    }
}