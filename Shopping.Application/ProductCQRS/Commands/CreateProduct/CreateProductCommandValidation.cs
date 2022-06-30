using FluentValidation;

namespace Shopping.Application.ProductCQRS.Commands.CreateProduct
{
    public class CreateProductCommandValidation : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidation()
        {
            RuleFor(r => r.Name)
                .MinimumLength(10)
                .MaximumLength(100)
                .NotEmpty();
            RuleFor(r => r.Amount)
                .NotEmpty();
            RuleFor(r => r.ShoppingListId).NotEmpty();
        }
    }
}
