﻿using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetProvincesResponse : BaseApiResponse<List<ProvinceDto>>
    {
        public GetProvincesResponse(HttpStatusCode statusCode, string message, List<ProvinceDto> data) : base(statusCode, message, data)
        {

        }
    }
}