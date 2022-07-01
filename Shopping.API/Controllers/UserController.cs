using Microsoft.AspNetCore.Mvc;
using Shopping.API.Controllers.Interfaces;
using Shopping.Application.UserCQRS.Commands.CreateUser;

namespace Shopping.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController:BaseController
{
    [HttpPost("AddUser")]
    public async Task<IActionResult> CreateUser(CreateUserCommandRequest command)
    {
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}