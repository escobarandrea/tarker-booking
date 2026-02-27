using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tarker.Booking.Application.External.GetTokenJwt;

namespace Tarker.Booking.External.GetTokenJwt
{
    public class GetTokenJwtService(IConfiguration configuration) : IGetTokenJwtService
    {
        public string Execute(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string key = configuration["SecretKeyJwt"]!;
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new(signingKey, SecurityAlgorithms.HmacSha256Signature),
                Issuer = configuration["IssuerJwt"],
                Audience = configuration["AudienceJwt"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
