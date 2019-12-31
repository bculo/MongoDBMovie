using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TBP.Entities;
using TBP.Interfaces;
using TBP.Options;

namespace TBP.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly SecurityOptions _security;

        public TokenManager(IOptions<SecurityOptions> security)
        {
            _security = security?.Value ?? throw new ArgumentNullException(nameof(security));
        }

        /// <summary>
        /// Kreiraj JWT token
        /// </summary>
        public string CreateJWTToken(User user)
        {
            List<Claim> claims = PrepareClaims(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_security.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _security.Issuer,
                _security.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(_security.AccessExpiration),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        /// <summary>
        /// Pripremi sadrzaj tokena (PAYLOAD)
        /// </summary>
        public List<Claim> PrepareClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.LowercaseName)
            };
        }
    }
}
