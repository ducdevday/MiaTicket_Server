using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.Data.Entity;
using MiaTicket.DataAccess;
using MiaTicket.DataAccess.Data;
using MiaTicket.DataCache;
using MiaTicket.Setting;
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
        public string GenerateAccessToken(string id, string email, string role);
        public string GenerateRefreshToken(string id, string email, string role);
        public void SetUserToken(string userId, string token, bool isUsed);
        public bool? GetUserToken(string userId, string token);
        public void DeleteUserToken(string userId, string token);
        public void DeleteAllUserToken(string userId);
    }

    public class TokenBusiness : ITokenBusiness
    {
        private readonly EnviromentSetting _setting;
        private readonly IDataAccessFacade _context;

        private readonly IRedisCacheService _cache;
        public const string PREFIX_KEY_USER_TOKEN = "user_token";
        private const string PREFIX_FORMAT = PREFIX_KEY_USER_TOKEN + ":{0}:{1}";
        public TokenBusiness(IDataAccessFacade context, EnviromentSetting setting, IRedisCacheService cache)
        {
            _setting = setting;
            _context = context;
            _cache = cache;
        }

        public async Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest request)
        {
            if(request.RefreshToken == null) return new GenerateTokenResponse(HttpStatusCode.BadRequest, "Invalid request", null);
            var principal = DecodeToken(request.RefreshToken);

            if (principal == null) return new GenerateTokenResponse(HttpStatusCode.BadRequest, "Invalid request", null);

            var id = principal.FindFirst("id")?.Value;
            var email = principal.FindFirst("email")?.Value;
            var role = principal.FindFirst("role")?.Value;
            if (principal == null || id == null || email == null || role == null) return new GenerateTokenResponse(HttpStatusCode.Unauthorized, "Invalid request", null);

            bool? isTokenUsed = GetUserToken(id, request.RefreshToken);

            if (isTokenUsed == null) return new GenerateTokenResponse(HttpStatusCode.Unauthorized, "Token not found in database", null);

            var accessToken = GenerateAccessToken(id, email, role);

            await _context.Commit();
            return new GenerateTokenResponse(HttpStatusCode.OK, "Generated Token", new RefreshTokenDataResponse(accessToken));
        }

        public string GenerateAccessToken(string id, string email, string role)
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

        public string GenerateRefreshToken(string id, string email, string role)
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

        private ClaimsPrincipal? DecodeToken(string token)
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

                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    return principal;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }

        public void SetUserToken(string userId, string token, bool isUsed)
        {
            string key = string.Format(PREFIX_FORMAT, userId, token);
            _cache.SetData(key, isUsed, TimeSpan.FromDays(AppConstant.REFRESH_TOKEN_EXPIRE_IN_DAYS));
        }

        public bool? GetUserToken(string userId, string token)
        {
            string key = string.Format(PREFIX_FORMAT, userId, token);
            return _cache.GetData<bool?>(key);
        }

        public void DeleteUserToken(string userId, string token) {
            string key = string.Format(PREFIX_FORMAT, userId, token);
            _cache.RemoveData(key);
        }

        public void DeleteAllUserToken(string userId)
        {
            string pattern = string.Format(PREFIX_FORMAT, userId, "*");
            _cache.RemoveDataBaseOnPattern(pattern);
        }
    }
}
