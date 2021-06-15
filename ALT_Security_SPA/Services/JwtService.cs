using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace ALT_Security_SPA.Services
{
    public class JwtService: IJwtService
    {
        private readonly JwtConfig _jwtConfig;

        public JwtService(IOptions<JwtConfig> jwtConfig)
        {
            _jwtConfig = jwtConfig.Value;
        }

        public string GenerateToken(List<Claim> claims, out DateTime expires)
        {
            string token = string.Empty;
            expires = DateTime.MinValue;

            if(claims != null && claims.Count() > 0)
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
                expires = DateTime.Now.AddDays(1);

                JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: _jwtConfig.Issuer,
                    audience: _jwtConfig.Audience,
                    expires: expires,
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                var jwtHandler = new JwtSecurityTokenHandler();
                token = jwtHandler.WriteToken(jwt);
            }

            return token;
        }
    }
}
