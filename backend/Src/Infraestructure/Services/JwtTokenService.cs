using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Src.Infraestructure.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Api.Src.Infraestructure.Services;

public class JwtTokenService(IConfiguration config)
{
    private readonly IConfiguration _config = config;
    public string GenerateToken(AppUser user)
    {
        List<Claim> claims = [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        ];

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var jwt = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: credentials
        );

        var handler = new JwtSecurityTokenHandler();

        return handler.WriteToken(jwt);
    }
}
