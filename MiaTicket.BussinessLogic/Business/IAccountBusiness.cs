using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.Data.Enum;
using MiaTicket.DataAccess;
using MiaTicket.Setting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace MiaTicket.BussinessLogic.Business
{
    public interface IAccountBusiness
    {
        public Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request);
        public Task<LoginResponse> Login(LoginRequest request);
        public Task<LogoutResponse> Logout(LogoutRequest request);
        public Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request);
    }

    public class AccountBusiness : IAccountBusiness
    {

        private readonly IDataAccessFacade _context;

        public AccountBusiness(IDataAccessFacade context)
        {
            _context = context;
        }

        public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request)
        {
            var validation = new CreateAccountValidation(request);
            validation.Validate();
            if (!validation.IsValid)
            {
                return new CreateAccountResponse(HttpStatusCode.BadRequest, validation.Message, string.Empty);
            }
            bool isGenderValid = await _context.UserData.IsGenderValid(request.Gender);
            if (!isGenderValid)
            {
                return new CreateAccountResponse(HttpStatusCode.BadRequest, "Invalid request", string.Empty);
            }
            bool isEmailExist = await _context.UserData.IsEmailExist(request.Email);
            if (isEmailExist)
            {
                return new CreateAccountResponse(HttpStatusCode.Conflict, "Email has already existed", string.Empty);
            }
            var addedEntity = await _context.UserData.CreateAccount(request.Name, request.Email, HashPassword(request.Password), request.PhoneNumber, request.BirthDate, request.Gender);
            await _context.Commit();
            return new CreateAccountResponse(HttpStatusCode.OK, "Register Successfully", addedEntity.Id.ToString());
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var validation = new LoginValidation(request);
            validation.Validate();
            if (!validation.IsValid)
            {
                return new LoginResponse(HttpStatusCode.BadRequest, validation.Message, null);
            }
            bool isEmailExist = await _context.UserData.IsEmailExist(request.Email);
            if (!isEmailExist)
            {
                return new LoginResponse(HttpStatusCode.Conflict, "Email hasn't existed", null);
            }

            var user = await _context.UserData.GetAccount(request.Email);
            if (user == null)
            {
                return new LoginResponse(HttpStatusCode.Conflict, "Email hasn't existed", null);
            }

            if (!ValidatePassword(request.Password, user.Password))
            {
                return new LoginResponse(HttpStatusCode.Unauthorized, "Wrong Password", null);
            }
            string accessToken = await GenerateAccessToken(user.Id.ToString(), user.Email, Enum.GetName(typeof(UserRole), user.Role));
            var refreshToken = await GenerateRefreshToken(user.Id.ToString(), user.Email, Enum.GetName(typeof(UserRole), user.Role));
            await _context.RefreshTokenData.SaveToken(refreshToken, user.Id);
            await _context.Commit();

            return new LoginResponse(HttpStatusCode.OK, "Login Successfully", new LoginDataResponse(new UserModel(user), accessToken, refreshToken));
        }

        public async Task<LogoutResponse> Logout(LogoutRequest request)
        {
            await _context.RefreshTokenData.ClearAllTokenByUserId(request.userId);
            await _context.Commit();
            return new LogoutResponse(HttpStatusCode.OK, "Logout Successfully", true);
        }

        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request)
        {
            var principal = DecodeToken(request.RefreshToken);
            var id = principal.FindFirst("id")?.Value;
            var email = principal.FindFirst("email")?.Value;
            var role = principal.FindFirst("role")?.Value;
            if (principal == null || id == null || email == null || role == null)
            {
                return new RefreshTokenResponse(HttpStatusCode.Unauthorized, "Invalid RefreshToken", null);
            }
            var currentRefreshToken = await _context.RefreshTokenData.GetLastTokenByUserId(new Guid(id));
            if (currentRefreshToken == null || currentRefreshToken.Value != request.RefreshToken || currentRefreshToken.IsDisable)
            {
                return new RefreshTokenResponse(HttpStatusCode.Unauthorized, "Invalid RefreshToken", null);
            }

            var accessToken = await GenerateAccessToken(id, email, role);
            var refreshToken = await GenerateRefreshToken(id, email, role);
            await _context.RefreshTokenData.DisableAllTokenByUserId(new Guid(id));
            await _context.RefreshTokenData.SaveToken(refreshToken, new Guid(id));
            await _context.Commit();
            return new RefreshTokenResponse(HttpStatusCode.OK, "Refresh Token", new RefreshTokenDataResponse(accessToken, refreshToken));
        }

        private async Task<string> GenerateAccessToken(string id, string email, string role)
        {
            var _setting = EnviromentSetting.GetInstance();
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
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<string> GenerateRefreshToken(string id, string email, string role)
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
                expires: DateTime.Now.AddMinutes(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ClaimsPrincipal DecodeToken(string token)
        {
            var _setting = EnviromentSetting.GetInstance();

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

        private byte[] HashPassword(string value)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(value);

            byte[] hashedBytes = Encoding.UTF8.GetBytes(hashedPassword);
            return hashedBytes;
        }


        private bool ValidatePassword(string raw, byte[] hashedBytes)
        {
            string hashedPassword = Encoding.UTF8.GetString(hashedBytes);
            return BCrypt.Net.BCrypt.Verify(raw, hashedPassword);
        }
    }
}
