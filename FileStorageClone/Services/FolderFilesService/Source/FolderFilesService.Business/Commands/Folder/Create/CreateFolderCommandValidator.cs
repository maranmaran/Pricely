using System.Runtime.CompilerServices;
using FluentValidation;

namespace FolderFilesService.Business.Commands.Folder.Create
{
    public class CreateFolderCommandValidator : AbstractValidator<CreateFolderCommand>
    {
        public CreateFolderCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}