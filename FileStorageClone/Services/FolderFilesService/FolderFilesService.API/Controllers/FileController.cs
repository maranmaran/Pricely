using FolderFilesService.API.Controllers;
using FolderFilesService.Business.Commands.File.Create;
using FolderFilesService.Business.Commands.File.Delete;
using FolderFilesService.Business.Queries.File.GetAll;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FileFilesService.API.Controllers
{
    /// <summary>
    /// Handles file management 
    /// </summary>
    public class FileController : BaseController
    {

        /// <summary>
        /// Retrieves files
        /// </summary>
        /// <remarks>
        /// Retrieves all files if no query parameters are specified
        /// </remarks>
        /// <param name="name">Filters files by name by start with logic</param>
        /// <param name="parentFolderId">Defines which folder to query for files</param>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string name, [FromQuery] Guid? parentFolderId, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new GetFilesQuery(parentFolderId, name), cancellationToken));
        }

        /// <summary>
        /// Creates file
        /// </summary>
        /// <remarks>
        /// Creates file that can be placed on file
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFileCommand command, CancellationToken cancellationToken = default)
        {
            // TODO: Refactor this to created at..
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        /// <summary>
        /// Delete file
        /// </summary>
        /// <remarks>
        /// Deletes file
        /// Publishes event that file has been changed
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new DeleteFileCommand(id), cancellationToken));
        }
    }
}
