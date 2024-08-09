using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.DataAccess;
using MiaTicket.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Business
{
    public interface IAccountBusiness
    {
        Task<RegisterResponse> Register(RegisterRequest request);
        Task<LoginResponse> Login(LoginRequest request);

    }

    public class AccountBusiness : IAccountBusiness
    {

        private readonly IDataAccessFacade _context;

        public AccountBusiness(IDataAccessFacade context)
        {
            _context = context;
        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            //TODO: Valid input Data
            if (await _context.UserData.IsAccountExist(request.Email))
            {
                return new RegisterResponse() { StatusCode = 409, Message = "Account has already exist", Data = false };

            }
            await _context.UserData.CreateAccount(request.Name, request.Email, request.Password, request.PhoneNumber,request.BirthDate,request.Gender);
            return new RegisterResponse()
            {
                StatusCode = 200,
                Message = "Success",
                Data = true
            };
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            
            if (await _context.UserData.IsAccountExist(request.Email))
            {
                return new LoginResponse() { StatusCode = 409, Message = "Account not already exist", Data = false };
            }
            var result = await _context.UserData.LoginAccount(request.Email, request.Password);
            if (result) return new LoginResponse() { StatusCode = 200, Message = "Success", Data = true };
            return new LoginResponse() { StatusCode = 200, Message = "Failure", Data = false };

        }
    }
}
