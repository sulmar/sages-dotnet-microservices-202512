using IdentityProvider.Api.Abstractions;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace IdentityProvider.Api.Infrastructure;

// dotnet Microsoft.IdentityModel.JsonWebTokens
public class JwtTokenService : ITokenService
{
    public string GenerateAccessToken(UserIdentity userIdentity)
    {
        var claims = new Dictionary<string, object>
        {
            [JwtRegisteredClaimNames.Jti] = Guid.NewGuid().ToString(),
            [JwtRegisteredClaimNames.Name] = userIdentity.UserName,
            [JwtRegisteredClaimNames.Email] = userIdentity.Email,
            [JwtRegisteredClaimNames.PhoneNumber] = userIdentity.Phone,
            [JwtRegisteredClaimNames.GivenName] = userIdentity.FirstName,
            [JwtRegisteredClaimNames.FamilyName] = userIdentity.LastName,
            [ClaimTypes.Role] = "developer",            
        };

        string secretKey = "a-string-secret-at-least-256-bits-long";

        var descriptor = new SecurityTokenDescriptor
        {
            Issuer = "https://sages.pl",
            Audience = "https://example.com",
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            SecurityAlgorithms.HmacSha256Signature),
            Claims = claims
        };

        var token = new JsonWebTokenHandler().CreateToken(descriptor);

        return token;
    }
}
