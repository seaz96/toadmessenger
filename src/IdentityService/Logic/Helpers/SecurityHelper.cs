using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Dal.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Logic.Helpers;

public class SecurityHelper
{
    public static string GetAuthToken(UserEntity user, IConfiguration configuration)
    {
        var userId = user.Id.ToString();
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId),
        };
        
        var secretKey = configuration.GetSection("Security")["SecurityKey"];
        if (secretKey is null)
        {
            throw new ArgumentNullException();
        }
        var key = Encoding.ASCII.GetBytes(secretKey);

        var jwt = new JwtSecurityToken(
            claims: claims,
            issuer: "Default",
            audience: "Default",
            expires: DateTime.UtcNow + TimeSpan.FromDays(7),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return token;
    }
}