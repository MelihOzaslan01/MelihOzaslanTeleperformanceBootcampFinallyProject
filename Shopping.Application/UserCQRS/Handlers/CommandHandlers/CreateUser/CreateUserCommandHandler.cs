using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Shopping.Application.UserCQRS.Commands.CreateUser;
using Shopping.Domain.Entities;

namespace Shopping.Application.UserCQRS.Handlers.CommandHandlers.CreateUser;

public class CreateUserCommandHandler:IRequestHandler<CreateUserCommandRequest,CreateUserCommandResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IDistributedCache _distributedCache;

    public CreateUserCommandHandler(UserManager<User> userManager, IDistributedCache distributedCache, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _distributedCache = distributedCache;
        _roleManager = roleManager;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        var createUserCommandResponse = new CreateUserCommandResponse();
        //TODO MAPPING TO ALL COMMAND REQ TO ENTITY ALSO REVERSE
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = request.Password,
            Email = request.Email,
            UserName = request.Email
        };
        var userToFind = await _userManager.FindByEmailAsync(user.Email);
        if (userToFind!=null)
        {
            createUserCommandResponse.IsSuccess = false;
            return createUserCommandResponse;
        }
        var createUserResult = await _userManager.CreateAsync(user,user.Password);
        if (!createUserResult.Succeeded)
        {
            createUserCommandResponse.IsSuccess = false;
            return createUserCommandResponse;
        }

        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name,user.Email));
        claims.Add(new Claim(ClaimTypes.Role,"User"));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        var addClaimResult = await _userManager.AddClaimsAsync(user, claims);
        if (!addClaimResult.Succeeded)
        {
            createUserCommandResponse.IsSuccess = false;
            return createUserCommandResponse;
        }

        createUserCommandResponse.IsSuccess = true;
        createUserCommandResponse.User = user;

        if (createUserCommandResponse.IsSuccess)
        {
            await _distributedCache.RemoveAsync("users", cancellationToken);
        }
        
        return createUserCommandResponse;
    }
}