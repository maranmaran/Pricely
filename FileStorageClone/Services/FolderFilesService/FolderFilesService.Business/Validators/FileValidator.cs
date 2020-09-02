using FluentValidation;
using FolderFilesService.Persistence.DTOModels;

namespace FolderFilesService.Business.Validators
{
    public class FileValidator : AbstractValidator<FileDto>
    {
        public FileValidator()
        {
           
        }
    }
}