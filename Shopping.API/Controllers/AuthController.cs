using Microsoft.AspNetCore.Mvc;
using Shopping.API.Controllers.Interfaces;
using Shopping.Application.Auth.Interfaces;
using Shopping.Domain.Dtos;

namespace Shopping.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController:BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginDto user)
    {
        var token = await _authService.Login(user);
        if (token==null)
        {
            return Unauthorized("Unauthorized!");
        }

        return Ok(token);
    }
}