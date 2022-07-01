using Shopping.Domain.Entities;

namespace Shopping.Application.UserCQRS.Commands.CreateUser;

public class CreateUserCommandResponse
{
    public bool IsSuccess { get; set; }
    public User User { get; set; }
}