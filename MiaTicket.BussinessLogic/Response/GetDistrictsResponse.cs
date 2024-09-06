﻿using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetDistrictsResponse : BaseApiResponse<List<DistrictDto>>
    {
        public GetDistrictsResponse(HttpStatusCode statusCode, string message, List<DistrictDto> data) : base(statusCode, message, data)
        {
        }
    }
}
