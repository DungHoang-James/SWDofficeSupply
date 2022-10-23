using FirebaseAdmin.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OfficeSupply.Business.Configure;
using OfficeSupply.Business.IServices;
using OfficeSupply.Data.Entities;
using OfficeSupply.Data.IRepository;
using OfficeSupply.Data.Models.Requests;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly ITokenRepository _tokenRepository;

        public TokenService(IOptionsMonitor<JwtConfig> options, ITokenRepository tokenRepository)
        {
            this._jwtConfig = options.CurrentValue;
            this._tokenRepository = tokenRepository;
        }

        public string GenerateJwtToken(User user)
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("FirstName", user.Firstname),
                new Claim("LastName", user.Lastname),
                new Claim("ID", user.ID.ToString()),
                new Claim(ClaimTypes.Role, user.RoleID.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signIn
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }

        //public string GenerateRefreshToken()
        //{
        //    int length = 35;
        //    var random = new Random();
        //    var chars = "0123456789QWERTYUIOPASDFGHJKLZXCVBNM";

        //    return new string(Enumerable.Repeat(chars, length).Select(x => x[random.Next(x.Length)]).ToArray()) + Guid.NewGuid();
        //}

        //public void AddToken(int userID, string jwtToken, string refreshToken)
        //{
        //    Token newToken = new()
        //    {
        //        UserID = userID,
        //        JwtToken = jwtToken,
        //        RefreshToken = refreshToken,
        //        IsUsed = false,
        //        CreateDate = DateTime.UtcNow,
        //        ExpiryDate = DateTime.UtcNow.AddMonths(1)
        //    };

        //    _tokenRepository.AddToken(newToken);
        //}

        //public Token CheckRefreshToken(RefreshToken refreshToken)
        //{
        //    return _tokenRepository.GetToken(refreshToken);
        //}

        //public void ExpiredToken(Token token)
        //{
        //    token.IsUsed = true;
        //    _tokenRepository.UpdateToken(token);
        //}
    }
}
