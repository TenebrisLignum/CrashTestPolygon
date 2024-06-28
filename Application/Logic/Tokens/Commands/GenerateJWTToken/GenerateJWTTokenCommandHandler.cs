using Application.Logic.Tokens.Commands.GenerateJWTToken;
using Application.Messaging;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public sealed class GenerateJWTTokenCommandHandler : ICommandHandler<GenerateJWTTokenCommand, string>
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;

    public GenerateJWTTokenCommandHandler(
        IConfiguration configuration, 
        UserManager<ApplicationUser> userManager 
    )
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<string> Handle(GenerateJWTTokenCommand request, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.User.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var roles = await _userManager.GetRolesAsync(request.User);

        foreach (var role in roles)
        {            
            claims.Add(new Claim("role", role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
