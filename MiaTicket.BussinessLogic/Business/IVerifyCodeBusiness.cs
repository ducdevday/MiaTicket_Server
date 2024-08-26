using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.Data.Enum;
using MiaTicket.DataAccess;
using MiaTicket.Email.Model;
using MiaTicket.Email.Template;
using MiaTicket.Email;
using MiaTicket.WebAPI.Constant;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System;
namespace MiaTicket.BussinessLogic.Business
{
    public interface IVerifyCodeBusiness
    {
        public Task<SendVerifyCodeResponse> SendVerifyCode(SendVerifyCodeRequest request);
        public Task<UseVerifyCodeResponse> UseVerifyCode(UseVerifyCodeRequest request);
        public string GenerateRandomString(int length);
    }

    public class VerifyCodeBusiness : IVerifyCodeBusiness
    {
        private readonly IDataAccessFacade _context;
        private readonly EmailService _emailService = EmailService.GetInstance();
        public VerifyCodeBusiness(IDataAccessFacade context)
        {
            _context = context;
        }

        public async Task<SendVerifyCodeResponse> SendVerifyCode(SendVerifyCodeRequest request)
        {
            var validation = new SendVerifyCodeValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new SendVerifyCodeResponse(HttpStatusCode.BadRequest, validation.Message, false);
            var user = await _context.UserData.GetAccountByEmail(request.Email);
            if (user == null) return new SendVerifyCodeResponse(HttpStatusCode.NotFound, "Account not found", false);
            var addedVerifyCode = await _context.VerifyCodeData.CreateVerifyCode(user.Id, GenerateRandomString(AppConstant.VERIFY_CODE_LENGHT), DateTime.Now.AddMinutes(AppConstant.VERIFY_CODE_EXPIRE_IN_MINUTES), (VerifyType)request.Type);
            await _context.Commit();
            switch ((VerifyType)request.Type)
            {
                case VerifyType.Register:
                    {
                        string activelink = $"{AppConstant.EMAIL_VERIFY_FINISH_PATH}?email={request.Email}&code={addedVerifyCode}";
                        await _emailService.Push(new ActivateEmail()
                        {
                            Sender = "MiaTicket@email.com",
                            Receiver = request.Email,
                            Body = ActivateEmailTemplate.GetEmailVerifyTemplate().Replace("{BRAND}", "MiaTicket").Replace("{EXPIRE_IN}", $"{AppConstant.VERIFY_CODE_EXPIRE_IN_MINUTES}").Replace("{ACTIVATE_URL}", activelink),
                            Subject = "<MiaTicket>Your email address verification"
                        });
                        break;
                    }
                case VerifyType.ResetPassword:
                    {
                        string resetPasswordLink = $"{AppConstant.RESET_PASSWORD_PATH}?email={request.Email}&code={addedVerifyCode}";
                        await _emailService.Push(new ResetPasswordEmail()
                        {
                            Sender = "MiaTicket@email.com",
                            Receiver = request.Email,
                            Body = ResetPasswordEmailTemplate.GetResetPasswordEmailTemplate().Replace("{BRAND}", "MiaTicket").Replace("{EXPIRE_IN}", $"{AppConstant.VERIFY_CODE_EXPIRE_IN_MINUTES}").Replace("{SET_PASSWORD_URL}", resetPasswordLink),
                            Subject = "<MiaTicket>Confirm Reset Password"
                        });
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return new SendVerifyCodeResponse(HttpStatusCode.OK, "Send Verify Successfully", true);
        }
        public async Task<UseVerifyCodeResponse> UseVerifyCode(UseVerifyCodeRequest request)
        {
            var validation = new UseVerifyCodeValidation(request);
            validation.Validate();

            if (!validation.IsValid) return new UseVerifyCodeResponse(HttpStatusCode.BadRequest, validation.Message, false);
            var user = await _context.UserData.GetAccountByEmail(request.Email);
            if (user == null) return new UseVerifyCodeResponse(HttpStatusCode.NotFound, "Account not found", false);
            var isValidCode = await _context.VerifyCodeData.IsValidCode(user.Id, request.Code, (VerifyType)request.Type);
            if (!isValidCode) return new UseVerifyCodeResponse(HttpStatusCode.Conflict, "Code is not valid", false);
            await _context.VerifyCodeData.UpdateUseOfCode(request.Code);
            await _context.Commit();

            return new UseVerifyCodeResponse(HttpStatusCode.OK, "Use Verify Successfully", true);
        }

        public string GenerateRandomString(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder result = new StringBuilder(length);
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    result.Append(validChars[(int)(num % (uint)validChars.Length)]);
                }
            }

            return result.ToString();
        }


    }
}
