using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shopping.Application.Auth.Interfaces;
using Shopping.Domain.Dtos;
using Shopping.Domain.Entities;

namespace Shopping.Application.Auth;

public class AuthService:IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<string> Login(UserLoginDto userLoginDto)
    {
        var userToLogin = await _userManager.FindByEmailAsync(userLoginDto.Email);
        if (userToLogin==null)
        {
            return null;
        }

        var userClaims = await _userManager.GetClaimsAsync(userToLogin);
        var claims = new List<Claim>();
        foreach (var claim in userClaims)
        {
            claims.Add(claim);
        }
        var token = GetToken(claims);

        var handler = new JwtSecurityTokenHandler();
        string jwt = handler.WriteToken(token);
        
        return jwt;
    }
    
    private JwtSecurityToken GetToken(List<Claim> claims)
    {

        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(

            signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256),
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            expires: DateTime.Now.AddDays(1),
            claims: claims
        );

        return token;

    }
}