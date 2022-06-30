using FluentValidation;
using Shopping.Application.ShoppingListCQRS.Commands.CreateShoppingList;

namespace Shopping.Application.ShoppingListCQRS.Commands.UpdateShoppingList;

public class UpdateShoppingListCommandValidation:AbstractValidator<CreateShoppingListCommandRequest>
{
    public UpdateShoppingListCommandValidation()
    {
        RuleFor(x => x.Category).MinimumLength(3).MaximumLength(250).NotEmpty();
        RuleFor(x => x.Description).MinimumLength(3).MaximumLength(250).NotEmpty();
        RuleFor(x => x.Title).MinimumLength(3).MaximumLength(50).NotEmpty();
    }
}