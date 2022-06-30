using FluentValidation;

namespace Shopping.Application.ProductCQRS.Commands.UpdateProduct
{
    public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandValidation()
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
