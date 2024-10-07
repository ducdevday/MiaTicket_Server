using AutoMapper;
using Azure.Core;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.CloudinaryStorage;
using MiaTicket.CloudinaryStorage.Model;
using MiaTicket.Data.Enum;
using MiaTicket.DataAccess;
using MiaTicket.Email;
using MiaTicket.Email.Model;
using MiaTicket.Email.Template;
using MiaTicket.WebAPI.Constant;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace MiaTicket.BussinessLogic.Business
{
    public interface IAccountBusiness
    {
        public Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request);
        public Task<LoginResponse> Login(LoginRequest request);
        public Task<LogoutResponse> Logout(LogoutRequest request);
        public Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request);
        public Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request);
        public Task<UpdateAccountResponse> UpdateAccount(Guid id, UpdateAccountRequest request);
        public Task<ActivateAccountResponse> ActivateAccount(ActivateAccountRequest request);
        public Task<GetAccountInformationResponse> GetAccountInformation(Guid userId);
    }

    public class AccountBusiness : IAccountBusiness
    {
        private readonly IDataAccessFacade _context;
        private readonly ITokenBusiness _tokenBusiness;
        private readonly IVerifyCodeBusiness _verifyCodeBusiness;
        private readonly ICloudinaryService _cloudinaryBusiness;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailProducer _emailProducer;
        public AccountBusiness(IDataAccessFacade context, ITokenBusiness tokenBusiness, IVerifyCodeBusiness verifyCodeBusiness, ICloudinaryService cloudinaryBusiness, IMapper mapper, IHttpContextAccessor httpContextAccessor, IEmailProducer emailProducer)
        {
            _context = context;
            _tokenBusiness = tokenBusiness;
            _verifyCodeBusiness = verifyCodeBusiness;
            _cloudinaryBusiness = cloudinaryBusiness;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _emailProducer = emailProducer;
        }

        public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request)
        {
            var validation = new CreateAccountValidation(request);
            validation.Validate();
            if (!validation.IsValid)
            {
                return new CreateAccountResponse(HttpStatusCode.BadRequest, validation.Message, false);
            }
            bool isGenderValid = await _context.UserData.IsGenderValid(request.Gender);
            if (!isGenderValid)
            {
                return new CreateAccountResponse(HttpStatusCode.BadRequest, "Invalid request", false);
            }
            bool isEmailExist = await _context.UserData.IsEmailExist(request.Email);
            if (isEmailExist)
            {
                return new CreateAccountResponse(HttpStatusCode.Conflict, "Email has already existed", false);
            }
            var addedUser = await _context.UserData.CreateAccount(request.Name, request.Email, HashPassword(request.Password), request.PhoneNumber, request.BirthDate, request.Gender);
            var addedVerifyCode = await _context.VerifyCodeData.CreateVerifyCode(addedUser.Id, _verifyCodeBusiness.GenerateRandomString(AppConstant.VERIFY_CODE_LENGHT), DateTime.Now.AddMinutes(AppConstant.VERIFY_CODE_EXPIRE_IN_MINUTES), VerifyType.Register);
            await _context.Commit();
            string activelink = $"{AppConstant.EMAIL_VERIFY_FINISH_PATH}?email={request.Email}&code={addedVerifyCode}";
            var emailModel = new EmailModel()
            {
                Sender = "MiaTicket@email.com",
                Receiver = addedUser.Email,
                Body = ActivateEmailTemplate.GetEmailVerifyTemplate().Replace("{BRAND}", "MiaTicket").Replace("{EXPIRE_IN}", $"{AppConstant.VERIFY_CODE_EXPIRE_IN_MINUTES}").Replace("{ACTIVATE_URL}", activelink),
                Subject = "<MiaTicket>Your email address verification"
            };
            _emailProducer.SendMessage(emailModel);

            return new CreateAccountResponse(HttpStatusCode.OK, "Register Successfully", true);
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

            var user = await _context.UserData.GetAccountByEmail(request.Email);
            if (user == null)
            {
                return new LoginResponse(HttpStatusCode.Conflict, "Email hasn't existed", null);
            }

            if (!ValidatePassword(request.Password, user.Password))
            {
                return new LoginResponse(HttpStatusCode.Conflict, "Wrong Password", null);
            }

            if (user.Status == UserStatus.UnVerified)
            {
                return new LoginResponse(HttpStatusCode.Forbidden, "Email Not Verified", null);
            }

            string accessToken = _tokenBusiness.GenerateAccessToken(user.Id.ToString(), user.Email, Enum.GetName(typeof(Role), user.Role));
            string refreshToken = _tokenBusiness.GenerateRefreshToken(user.Id.ToString(), user.Email, Enum.GetName(typeof(Role), user.Role));

            _tokenBusiness.SetUserToken(user.Id.ToString(), refreshToken, false);

            _httpContextAccessor.HttpContext.Items["refreshToken"] = refreshToken;
            return new LoginResponse(HttpStatusCode.OK, "Login Successfully", new LoginDataResponse(user.Id, accessToken));
        }


        public async Task<LogoutResponse> Logout(LogoutRequest request)
        {
            var validation = new LogoutValidation(request);
            validation.Validate();

            if (!validation.IsValid) return new LogoutResponse(HttpStatusCode.BadRequest, validation.Message, false);

            var account = await _context.UserData.GetAccountById(request.userId);

            if (account == null) return new LogoutResponse(HttpStatusCode.Conflict, "Account does not exist", false);

            _tokenBusiness.DeleteUserToken(request.userId.ToString(), request.refreshToken);

            return new LogoutResponse(HttpStatusCode.OK, "Logout Successfully", true);
        }

        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request)
        {
            var validation = new ChangePasswordValidation(request);
            validation.Validate();
            if (!validation.IsValid)
            {
                return new ChangePasswordResponse(HttpStatusCode.BadRequest, validation.Message, false);
            }

            var user = await _context.UserData.GetAccountById(request.UserId);
            if (user == null)
            {
                return new ChangePasswordResponse(HttpStatusCode.Conflict, "Account does not exist", false);
            }

            if (!ValidatePassword(request.CurrentPassword, user.Password))
            {
                return new ChangePasswordResponse(HttpStatusCode.Conflict, "Wrong Password", false);
            }
            await _context.UserData.ChangePassword(request.UserId, HashPassword(request.NewPassword));
            await _context.Commit();
            return new ChangePasswordResponse(HttpStatusCode.OK, "Change Password SuccessFully", true);
        }


        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            var validation = new ResetPasswordValidation(request);
            validation.Validate();
            if (!validation.IsValid)
            {
                return new ResetPasswordResponse(HttpStatusCode.BadRequest, validation.Message, false);
            }

            var user = await _context.UserData.GetAccountByEmail(request.Email);
            if (user == null)
            {
                return new ResetPasswordResponse(HttpStatusCode.Conflict, "Account does not exist", false);
            }
            
            var isUsedCode = await _context.VerifyCodeData.IsUsedCode(request.Code);
            if (!isUsedCode) {
                return new ResetPasswordResponse(HttpStatusCode.Forbidden, "Not Confirm Reset Password In Email", false);
            }
            await _context.UserData.ChangePassword(user.Id, HashPassword(request.NewPassword));
            await _context.Commit();
            return new ResetPasswordResponse(HttpStatusCode.OK, "Reset Password SuccessFully", true);
        }

        public async Task<UpdateAccountResponse> UpdateAccount(Guid id, UpdateAccountRequest request)
        {
            var validation = new UpdateAccountValidation(request);
            validation.Validate();
            if (!validation.IsValid)
            {
                return new UpdateAccountResponse(HttpStatusCode.BadRequest, validation.Message, false);
            }

            bool isGenderValid = await _context.UserData.IsGenderValid(request.Gender);
            if (!isGenderValid)
            {
                return new UpdateAccountResponse(HttpStatusCode.BadRequest, "Invalid request", false);
            }

            var user = await _context.UserData.GetAccountById(id);
            if (user == null)
            {
                return new UpdateAccountResponse(HttpStatusCode.Conflict, "Account does not exist", false);
            }
            string? avatarUrl = null;
            if (request.AvatarFile != null) {
                avatarUrl = await _cloudinaryBusiness.UploadFileAsync(request.AvatarFile, FileType.AVATAR_IMAGE);
            }

            var updatedUser = await _context.UserData.UpdateAccount(id, request.Name, request.PhoneNumber, request.BirthDate, request.Gender, avatarUrl );
            await _context.Commit();

            return new UpdateAccountResponse(HttpStatusCode.OK, "Update Account Success", true);
        }

        public async Task<ActivateAccountResponse> ActivateAccount(ActivateAccountRequest request)
        {
            var validation = new ActivateAccountValidation(request);
            validation.Validate();
            if (!validation.IsValid)
            {
                return new ActivateAccountResponse(HttpStatusCode.BadRequest, validation.Message, false);
            }
            var user = await _context.UserData.GetAccountByEmail(request.Email);
            if (user == null)
            {
                return new ActivateAccountResponse(HttpStatusCode.Conflict, "Account does not exist", false);
            }
            if (user.Status != UserStatus.UnVerified) { 
                return new ActivateAccountResponse(HttpStatusCode.Conflict, "Account has already verify", false);
            }
            var isValidCode = await _context.VerifyCodeData.IsValidCode(user.Id, request.Code, VerifyType.Register);
            if (!isValidCode) { 
                return new ActivateAccountResponse(HttpStatusCode.Conflict, "Code is not valid", false);
            }
            await _context.VerifyCodeData.UpdateUseOfCode(request.Code);
            await _context.UserData.ActivateAccount(request.Email);
            await _context.Commit();
            return new ActivateAccountResponse(HttpStatusCode.OK, "Account Verify Success", true);
        }

        public async Task<GetAccountInformationResponse> GetAccountInformation(Guid userId)
        {
            var user = await _context.UserData.GetAccountById(userId);
            if (user == null)
            {
                return new GetAccountInformationResponse(HttpStatusCode.BadRequest, "Get Account Information Failed", null);
            }
            var dataResponse = _mapper.Map<UserDto>(user);
            return new GetAccountInformationResponse(HttpStatusCode.OK, "Get Account Information Succeed", dataResponse);
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
