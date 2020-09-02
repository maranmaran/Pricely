using FolderFilesService.Business.Commands.Folder.Create;
using FolderFilesService.Business.Commands.Folder.Delete;
using FolderFilesService.Business.Queries.Folder.Get;
using FolderFilesService.Business.Queries.Folder.GetAll;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FolderFilesService.API.Controllers
{
    /// <summary>
    /// Hanldes folder management
    /// </summary>
    public class FolderController : BaseController
    {

        /// <summary>
        /// Retrieves folder
        /// </summary>
        /// <remarks>
        /// Retrieves single folder
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetFolderQuery(id), cancellationToken));
        }

        /// <summary>
        /// Retrieves folders
        /// </summary>
        /// <remarks>
        /// Retrieves all folders if no query parameters are specified
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetFoldersQuery(), cancellationToken));
        }

        /// <summary>
        /// Creates folder
        /// </summary>
        /// <remarks>
        /// Creates folder that can be placed on folder
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFolderCommand command, CancellationToken cancellationToken = default)
        {
            // TODO: Refactor this to created at..
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        /// <summary>
        /// Delete folder
        /// </summary>
        /// <remarks>
        /// Deletes folder
        /// Publishes event that folder has been changed
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new DeleteFolderCommand(id), cancellationToken));
        }
    }
}