﻿using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class UpdateAccountResponse : BaseApiResponse<UserDto?>
    {
        public UpdateAccountResponse(HttpStatusCode statusCode, string message, UserDto? data) : base(statusCode, message, data)
        {
            
        }
    }
}
