using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NextLevelBJJ.Application.Students.DTO;
using NextLevelBJJ.Application.Students.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NextLevelBJJ.Infrastructure.Auth
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IOptions<JwtOptions> _options;
        private readonly SigningCredentials _signingCredentials;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecretKey));
            _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
        }

        public JwtDto CreateToken(string studentId, string role)
        {
            if(studentId is null)
            {
                throw new ArgumentException("Student id claim can not be empty.", nameof(studentId));
            }

            var now = DateTime.UtcNow;
            var nowEpoch = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, studentId),
                new Claim(JwtRegisteredClaimNames.UniqueName, studentId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, nowEpoch.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var expires = now.AddMinutes(_options.Value.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                AccessToken = token,
                Role = role ?? string.Empty
            };
        }
    }
}
