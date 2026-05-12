using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Entites.Identity;
using talabat.Core.Services.Contract;

namespace talabat.Service.Account;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> role)
    {

        var Claims = new List<Claim>()
        {
           new Claim(ClaimTypes.GivenName ,user.DisplayName),
           new Claim(ClaimTypes.Email , user.Email)
        };
        var UserRoles = await role.GetRolesAsync(user);
        foreach( var item in UserRoles)
        {
            Claims.Add(new Claim(ClaimTypes.Role , item));
        }

        var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

        var Token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: Claims,
            expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
            signingCredentials: new SigningCredentials(authKey ,SecurityAlgorithms.HmacSha256)
            );
         return new JwtSecurityTokenHandler().WriteToken(Token);
    }
}
