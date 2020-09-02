using FluentValidation;
using FolderFilesService.Business.Validators;

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