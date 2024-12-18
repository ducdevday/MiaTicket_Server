﻿using MiaTicket.BussinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Response
{
    public class GetDirectoryOrganizersResponse : BaseApiResponse<List<DirectoryOrganizerDto>>
    {
        public GetDirectoryOrganizersResponse(HttpStatusCode statusCode, string message, List<DirectoryOrganizerDto> data) : base(statusCode, message, data)
        {
        }
    }
}
