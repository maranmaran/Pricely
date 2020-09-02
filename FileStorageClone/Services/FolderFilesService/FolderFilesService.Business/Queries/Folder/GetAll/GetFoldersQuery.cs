using System.Collections.Generic;
using FolderFilesService.Persistence.DTOModels;
using MediatR;

namespace FolderFilesService.Business.Queries.Folder.GetAll
{
    public class GetFoldersQuery : IRequest<IEnumerable<FolderDto>>
    {

    }
}
