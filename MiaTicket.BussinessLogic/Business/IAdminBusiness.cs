using DocumentFormat.OpenXml.Spreadsheet;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Util;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using MiaTicket.DataAccess;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace MiaTicket.BussinessLogic.Business
{
    public interface IAdminBusiness
    {
        public Task<AdminLoginResponse> Login(AdminLoginRequest request);
        public Task<AdminChangeDefaultPasswordResponse> ChangeDefaultPassword(AdminChangeDefaultPasswordRequest request);
    }

    public class AdminBusiness : IAdminBusiness {
        private readonly IDataAccessFacade _context;
        private readonly ITokenBusiness _tokenBusiness;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminBusiness(IDataAccessFacade context, ITokenBusiness tokenBusiness, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _tokenBusiness = tokenBusiness;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AdminLoginResponse> Login(AdminLoginRequest request)
        {
            var validation = new AdminLoginValidation(request);
            if (!validation.IsValid)
            {
                return new AdminLoginResponse(HttpStatusCode.BadRequest, validation.Message, null);
            }

            var admin = await _context.AdminData.GetAdmin(request.Account);

            if (admin == null) {
                return new AdminLoginResponse(HttpStatusCode.NotFound, "Not Found Account", null);
            }

            if(!PasswordUtil.ValidatePassword(request.Password, admin.Password))
            {
                return new AdminLoginResponse(HttpStatusCode.Conflict, "Wrong Password", null);
            }

            if(admin.IsPasswordTemporary) {
                return new AdminLoginResponse(HttpStatusCode.BadRequest, "You must change your password before proceeding", null);
            }

            string accessToken = _tokenBusiness.GenerateAccessToken(admin.Id.ToString(), admin.Account, Enum.GetName(typeof(Role), Role.Admin));
            string refreshToken = _tokenBusiness.GenerateRefreshToken(admin.Id.ToString(), admin.Account, Enum.GetName(typeof(Role), Role.Admin));

            _tokenBusiness.SetUserToken(admin.Id.ToString(), refreshToken, false);

            _httpContextAccessor.HttpContext.Items["refreshToken"] = refreshToken;

            var loginDataDto = new LoginDataDto()
            {
                UserId = admin.Id,
                AccessToken = accessToken,
                Role = Role.Admin,
            };

            return new AdminLoginResponse(HttpStatusCode.OK, "Login Success", loginDataDto);
        }

        public async Task<AdminChangeDefaultPasswordResponse> ChangeDefaultPassword(AdminChangeDefaultPasswordRequest request)
        {
            var validation = new AdminChangeDefaultPasswordValidation(request);
            if (!validation.IsValid) {
                return new AdminChangeDefaultPasswordResponse(HttpStatusCode.BadRequest, validation.Message, false);
            }

            var admin = await _context.AdminData.GetAdmin(request.Account);

            if (admin == null) { 
                return new AdminChangeDefaultPasswordResponse(HttpStatusCode.NotFound, "Not Found Account", false);
            }

            if (!PasswordUtil.ValidatePassword(request.DefaultPassword, admin.Password)){
                return new AdminChangeDefaultPasswordResponse(HttpStatusCode.Conflict, "Wrong Password", false);
            }

            if (!admin.IsPasswordTemporary) {
                return new AdminChangeDefaultPasswordResponse(HttpStatusCode.BadRequest, "You changed default password", false);
            }

            admin.Password = PasswordUtil.HashPassword(request.NewPassword);
            admin.IsPasswordTemporary = false;
            await _context.AdminData.UpdateAdmin(admin);
            await _context.Commit();
            return new AdminChangeDefaultPasswordResponse(HttpStatusCode.OK, "Change Default Password SuccessFully", true);
        }

    }
}
