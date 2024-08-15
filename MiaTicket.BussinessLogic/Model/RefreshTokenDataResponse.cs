using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class RefreshTokenDataResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public RefreshTokenDataResponse(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
