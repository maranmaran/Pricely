using FluentValidation;
using FolderFilesService.Business.Validators;

namespace FolderFilesService.Business.Commands.File.Create
{
    public class CreateFileCommandValidator : AbstractValidator<CreateFileCommand>
    {
        public CreateFileCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}