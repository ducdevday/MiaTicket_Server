﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class GetOrderReportRequest : BaseApiRequest
    {
        public int ShowTimeId { get; set; }
    }
}
