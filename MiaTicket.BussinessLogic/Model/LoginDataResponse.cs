using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class LoginDataResponse
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }

        public LoginDataResponse(Guid userId, string accessToken)
        {
            this.UserId = userId;
            this.AccessToken = accessToken;
        }
    }
}
