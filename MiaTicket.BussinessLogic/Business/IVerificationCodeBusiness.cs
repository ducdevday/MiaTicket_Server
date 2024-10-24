using CloudinaryDotNet;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using MiaTicket.DataAccess;
using MiaTicket.Email;
using MiaTicket.Email.Model;
using MiaTicket.Email.Template;
using MiaTicket.Setting;
using System.Net;
using System.Security.Cryptography;
using System.Text;
namespace MiaTicket.BussinessLogic.Business
{
    public interface IVerificationCodeBusiness
    {
        public Task<SendVerificationCodeResponse> SendVerificationCode(SendVerificationCodeRequest request);
        public Task<UseVerifcationCodeResponse> UseVerificationCode(UseVeriicationCodeRequest request);
        public string GenerateRandomString(int length);
    }

    public class VerificationCodeBusiness : IVerificationCodeBusiness
    {
        private readonly IDataAccessFacade _context;
        private readonly IEmailProducer _emailProducer;
        public VerificationCodeBusiness(IDataAccessFacade context, IEmailProducer emailProducer)
        {
            _context = context;
            _emailProducer = emailProducer;
        }

        public async Task<SendVerificationCodeResponse> SendVerificationCode(SendVerificationCodeRequest request)
        {
            var validation = new SendVerificationCodeValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new SendVerificationCodeResponse(HttpStatusCode.BadRequest, validation.Message, false);
            var user = await _context.UserData.GetAccountByEmail(request.Email);
            if (user == null) return new SendVerificationCodeResponse(HttpStatusCode.NotFound, "Account not found", false);

            var verificationCode = new VerificationCode()
            {
                UserId = user.Id,
                Code = GenerateRandomString(AppConstant.VERIFICATION_CODE_LENGHT),
                ExpireAt = DateTime.Now.AddMinutes(AppConstant.VERIFICATION_CODE_EXPIRE_IN_MINUTES),
                Type = (VerificationType)request.Type
            };
            await _context.VerificationCodeData.CreateVerificationCode(verificationCode);
            await _context.Commit();
            switch ((VerificationType)request.Type)
            {
                case VerificationType.Register:
                    {
                        string activelink = $"{AppConstant.EMAIL_VERIFY_FINISH_PATH}?email={request.Email}&code={verificationCode.Code}";
                        EmailModel emailModel = new EmailModel()
                        {
                            Sender = "MiaTicket@email.com",
                            Receiver = request.Email,
                            Body = ActivateEmailTemplate.GetEmailVerifyTemplate().Replace("{BRAND}", "MiaTicket").Replace("{EXPIRE_IN}", $"{AppConstant.VERIFICATION_CODE_EXPIRE_IN_MINUTES}").Replace("{ACTIVATE_URL}", activelink),
                            Subject = "<MiaTicket>Your email address verification"
                        };
                        _emailProducer.SendMessage(emailModel);
                        break;
                    }
                case VerificationType.ResetPassword:
                    {
                        string resetPasswordLink = $"{AppConstant.RESET_PASSWORD_PATH}?email={request.Email}&code={verificationCode.Code}";
                        EmailModel emailModel = new EmailModel()
                        {
                            Sender = "MiaTicket@email.com",
                            Receiver = request.Email,
                            Body = ResetPasswordEmailTemplate.GetResetPasswordEmailTemplate().Replace("{BRAND}", "MiaTicket").Replace("{EXPIRE_IN}", $"{AppConstant.VERIFICATION_CODE_EXPIRE_IN_MINUTES}").Replace("{SET_PASSWORD_URL}", resetPasswordLink),
                            Subject = "<MiaTicket>Confirm Reset Password"
                        };
                        _emailProducer.SendMessage(emailModel);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return new SendVerificationCodeResponse(HttpStatusCode.OK, "Send Verify Successfully", true);
        }
        public async Task<UseVerifcationCodeResponse> UseVerificationCode(UseVeriicationCodeRequest request)
        {
            var validation = new UseVerificationCodeValidation(request);
            validation.Validate();

            if (!validation.IsValid) return new UseVerifcationCodeResponse(HttpStatusCode.BadRequest, validation.Message, false);
            var user = await _context.UserData.GetAccountByEmail(request.Email);
            if (user == null) return new UseVerifcationCodeResponse(HttpStatusCode.NotFound, "Account not found", false);
            var isValidCode = await _context.VerificationCodeData.IsValidCode(user.Id, request.Code, (VerificationType)request.Type);
            if (!isValidCode) return new UseVerifcationCodeResponse(HttpStatusCode.Conflict, "Code is not valid", false);
            await _context.VerificationCodeData.UpdateUseOfCode(request.Code);
            await _context.Commit();

            return new UseVerifcationCodeResponse(HttpStatusCode.OK, "Use Verify Successfully", true);
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
