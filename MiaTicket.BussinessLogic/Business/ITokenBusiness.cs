using MiaTicket.Data.Entity;
using MiaTicket.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MiaTicket.BussinessLogic.Business
{
    public interface ITokenBusiness
    {
        public Task<string> GenerateToken();
    }

    public class TokenBusiness : ITokenBusiness
    {
        private readonly EnviromentSetting _setting;
        public TokenBusiness(EnviromentSetting setting)
        {
            _setting = setting;
        }

        public async Task<string> GenerateToken()
        {
            var claims = new[]
            {
                new Claim("email", "abc@example.com"),
                new Claim("role", "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_setting.GetSecret()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _setting.GetIssuer(),
                audience: _setting.GetAudience(),
                claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateAccessToken(User user) {
            var claims = new[]
{               
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("role", "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_setting.GetSecret()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _setting.GetIssuer(),
                audience: _setting.GetAudience(),
                claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken() {
            var claims = new[] {
                new Claim("email",  "abc@example.com"),
                new Claim("role", "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_setting.GetSecret()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _setting.GetIssuer(),
                audience: _setting.GetAudience(),
                claims,
                expires: DateTime.Now.AddMinutes(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }
}
