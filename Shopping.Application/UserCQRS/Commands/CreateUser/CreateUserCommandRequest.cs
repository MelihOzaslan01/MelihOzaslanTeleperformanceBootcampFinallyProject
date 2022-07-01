using MediatR;

namespace Shopping.Application.UserCQRS.Commands.CreateUser;

public class CreateUserCommandRequest:IRequest<CreateUserCommandResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}