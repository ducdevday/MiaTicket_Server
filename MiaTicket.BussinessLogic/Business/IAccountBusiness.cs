using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.DataAccess;
using MiaTicket.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Business
{
    public interface IAccountBusiness
    {
        Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request);

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
            if (!validation.IsValid) {
                return new CreateAccountResponse(HttpStatusCode.BadRequest, validation.Message, string.Empty);
            }
            bool isGenderValid = await _context.UserData.IsGenderValid(request.Gender);
            if (!isGenderValid)
            {
                return new CreateAccountResponse(HttpStatusCode.BadRequest, "Invalid request", string.Empty);
            }
            bool isEmailExist = await _context.UserData.IsEmailExist(request.Email);
            if (isEmailExist) {
                return new CreateAccountResponse(HttpStatusCode.Conflict, "Email has already existed", string.Empty);
            }
            var addedEntity = await _context.UserData.CreateAccount(request.Name, request.Email, request.Password,request.PhoneNumber,request.BirthDate,request.Gender);

            await _context.Commit();
            return new CreateAccountResponse(HttpStatusCode.OK, string.Empty, addedEntity.Id.ToString());
        }
    }
}
