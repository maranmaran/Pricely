using MenuService.Business.Commands.Menu.Create;
using MenuService.Business.Commands.Menu.Delete;
using MenuService.Business.Commands.Menu.Update;
using MenuService.Business.Queries.Menu.Get;
using MenuService.Business.Queries.Menu.GetAll;
using MenuService.Persistence.DTOModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MenuService.API.Controllers
{
    public class MenuController : BaseController
    {

        /// <summary>
        /// Retrieves menu
        /// </summary>
        /// <remarks>
        /// Retrieves single menu
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetMenuQuery(id), cancellationToken));
        }

        /// <summary>
        /// Retrieves menus
        /// </summary>
        /// <remarks>
        /// Retrieves all menus if no query parameters are specified
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetMenusQuery(), cancellationToken));
        }

        /// <summary>
        /// Creates menu
        /// </summary>
        /// <remarks>
        /// Creates menu that can be placed on menu
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] MenuDto menu, CancellationToken cancellationToken = default)
        {
            // TODO: Refactor this to created at..uri + query params... also change 200OK status produce
            return Ok(await Mediator.Send(new CreateMenuCommand(menu), cancellationToken));
        }

        /// <summary>
        /// Updates menu
        /// </summary>
        /// <remarks>
        /// Updates menu that can be placed on menu
        /// Publishes event that menu has been changed
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] MenuDto menu, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new UpdateMenuCommand(menu), cancellationToken));
        }

        /// <summary>
        /// Delete menu
        /// </summary>
        /// <remarks>
        /// Deletes menu
        /// Publishes event that menu has been changed
        /// </remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new DeleteMenuCommand(id), cancellationToken));
        }
    }
}