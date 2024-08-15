﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class LoginDataResponse
    {
        public UserModel User { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public LoginDataResponse(UserModel user, string accessToken, string refreshToken)
        {
            User = user;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
