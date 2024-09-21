﻿using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetOrderDetailResponse : BaseApiResponse<OrderDetailDto?>
    {
        public GetOrderDetailResponse(HttpStatusCode statusCode, string message, OrderDetailDto? data) : base(statusCode, message, data)
        {
        }
    }
}
