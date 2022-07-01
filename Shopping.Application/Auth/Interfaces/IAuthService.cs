using Shopping.Domain.Dtos;

namespace Shopping.Application.Auth.Interfaces;

public interface IAuthService
{
    public Task<string> Login(UserLoginDto userLoginDto);
}