using ItemService.Business.Commands.Items.Create;
using ItemService.Business.Commands.Items.Delete;
using ItemService.Business.Commands.Items.Update;
using ItemService.Business.Queries.Items.GetItem;
using ItemService.Business.Queries.Items.GetItems;
using ItemService.Persistence.DTOModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ItemService.API.Controllers
{
    public class ItemController : BaseController
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
            return Ok(await Mediator.Send(new GetItemQuery(id), cancellationToken));
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
            return Ok(await Mediator.Send(new GetItemsQuery(), cancellationToken));
        }

        /// <summary>
        /// Creates item
        /// </summary>
        /// <remarks>
        /// Creates item that can be placed on menu
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemDto item, CancellationToken cancellationToken = default)
        {
            // TODO: Refactor this to created at..
            return Ok(await Mediator.Send(new CreateItemCommand(item), cancellationToken));
        }

        /// <summary>
        /// Updates item
        /// </summary>
        /// <remarks>
        /// Updates item that can be placed on menu
        /// Publishes event that item has been changed
        /// </remarks>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ItemDto item, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new UpdateItemCommand(item), cancellationToken));
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
            return Ok(await Mediator.Send(new DeleteItemCommand(id), cancellationToken));
        }
    }
}
