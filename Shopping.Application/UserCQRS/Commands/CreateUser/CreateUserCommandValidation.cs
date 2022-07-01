using FluentValidation;
using Shopping.Domain.Entities;

namespace Shopping.Application.UserCQRS.Commands.CreateUser;

public class CreateUserCommandValidation:AbstractValidator<CreateUserCommandRequest>
{
    public CreateUserCommandValidation()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}