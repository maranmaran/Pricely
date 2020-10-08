using FluentValidation;
using ItemService.Business.Validators;

namespace ItemService.Business.Commands.Items.Update
{
    public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemCommandValidator()
        {
            RuleFor(x => x.Item).SetValidator(new ItemValidator());

            // for update only validating properly assigned IDs is important.. as they are the only thing getting mapped
            // we can't update their own properties (Category, Ingredients and Allergens) in ItemUpdateCommand..
            // we have their own controllers for that
            RuleFor(x => x.Item.Category.Id).NotEmpty();

            RuleForEach(x => x.Item.Ingredients)
                .ChildRules(validator =>
                    {
                        validator.RuleFor(x => x.Id).NotEmpty();
                    }
                );

            RuleForEach(x => x.Item.Allergens)
                .ChildRules(validator =>
                {
                    validator.RuleFor(x => x.Id).NotEmpty();
                }
            ); ;
        }
    }
}