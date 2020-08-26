using System.Threading;
using System.Threading.Tasks;
using IdentityService.Business.Commands.Items.CreateItem;
using IdentityService.Business.Queries.Items.GetItems;
using IdentityService.Persistence.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers
{
    public class ItemsController : BaseController
    {
        /// <summary>
        /// Retrieves items
        /// </summary>
        /// <remarks>
        /// Retrieves all items if no query parameters are specified
        /// </remarks>
        /// <param name="cancellationToken"></param>
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
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
        public async Task<IActionResult> Create(ItemDto item, CancellationToken cancellationToken = default)
        {
            // TODO: Refactor this to created at..
            return Ok(await Mediator.Send(new CreateItemCommand(item), cancellationToken));
        }
    }
}
