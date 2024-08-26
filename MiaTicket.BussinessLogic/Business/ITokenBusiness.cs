using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.Data.Entity;
using MiaTicket.DataAccess;
using MiaTicket.Setting;
using MiaTicket.WebAPI.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MiaTicket.BussinessLogic.Business
{
    public interface ITokenBusiness
    {
        public Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest request);
        public Task<string> GenerateAccessToken(string id, string email, string role);
        public Task<string> GenerateRefreshToken(string id, string email, string role);

    }

    public class TokenBusiness : ITokenBusiness
    {
        private readonly EnviromentSetting _setting;
        private readonly IDataAccessFacade _context;
        public TokenBusiness(IDataAccessFacade context, EnviromentSetting setting)
        {
            _setting = setting;
            _context = context;
        }

        public async Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest request)
        {
            if(request.RefreshToken == null) return new GenerateTokenResponse(HttpStatusCode.BadRequest, "Invalid request", null);
            var principal = DecodeToken(request.RefreshToken);

            if (principal == null) return new GenerateTokenResponse(HttpStatusCode.BadRequest, "Invalid request", null);

            var id = principal.FindFirst("id")?.Value;
            var email = principal.FindFirst("email")?.Value;
            var role = principal.FindFirst("role")?.Value;
            if (principal == null || id == null || email == null || role == null) return new GenerateTokenResponse(HttpStatusCode.Unauthorized, "Invalid RefreshToken", null);

            var currentRefreshToken = await _context.RefreshTokenData.GetLastTokenByUserId(new Guid(id));
            if (currentRefreshToken == null || currentRefreshToken.Value != request.RefreshToken || currentRefreshToken.IsDisable)
            {
                return new GenerateTokenResponse(HttpStatusCode.Unauthorized, "Invalid RefreshToken", null);
            }

            var accessToken = await GenerateAccessToken(id, email, role);
            var refreshToken = await GenerateRefreshToken(id, email, role);
            await _context.RefreshTokenData.DisableAllTokenByUserId(new Guid(id));
            await _context.RefreshTokenData.SaveToken(refreshToken, new Guid(id));
            await _context.Commit();
            return new GenerateTokenResponse(HttpStatusCode.OK, "Generated Token", new RefreshTokenDataResponse(accessToken, refreshToken));
        }

        public async Task<string> GenerateAccessToken(string id, string email, string role)
        {
            var claims = new[]
            {
                new Claim("id", id),
                new Claim("email", email),
                new Claim("role", role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_setting.GetSecret()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _setting.GetIssuer(),
                audience: _setting.GetAudience(),
                claims,
                expires: DateTime.Now.AddMinutes(AppConstant.ACCESS_TOKEN_EXPIRE_IN_MINUTES),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken(string id, string email, string role)
        {
            var _setting = EnviromentSetting.GetInstance();
            var claims = new[] {
                new Claim("id", id),
                new Claim("email", email),
                new Claim("role", role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_setting.GetSecret()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _setting.GetIssuer(),
                audience: _setting.GetAudience(),
                claims,
                expires: DateTime.Now.AddDays(AppConstant.REFRESH_TOKEN_EXPIRE_IN_DAYS),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ClaimsPrincipal DecodeToken(string token)
        {

            var tokenHandler = new JwtSecurityTokenHandler()
            {
                MapInboundClaims = false,
            };
            var key = Encoding.UTF8.GetBytes(_setting.GetSecret());

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _setting.GetIssuer(),
                    ValidAudience = _setting.GetAudience(),
                    ClockSkew = TimeSpan.Zero // Optional: removes the default 5 min clock skew

                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                // Optionally, check if the token is indeed a JWT token
                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    // Here, the token is successfully validated and claims can be accessed
                    return principal;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}
