using FluentValidation;

namespace Shopping.Application.ShoppingListCQRS.Commands.CreateShoppingList;

public class CreateShoppingListCommandValidation:AbstractValidator<CreateShoppingListCommandRequest>
{
    public CreateShoppingListCommandValidation()
    {
        RuleFor(x => x.Category).MinimumLength(3).MaximumLength(250).NotEmpty();
        RuleFor(x => x.Description).MinimumLength(3).MaximumLength(250).NotEmpty();
        RuleFor(x => x.Title).MinimumLength(3).MaximumLength(50).NotEmpty();
    }
}