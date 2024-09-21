﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class ShowTimeDetailDto
    {
        public int Id { get; set; }
        public DateTime ShowStartAt { get; set; }
        public DateTime ShowEndAt { get; set; }
        public DateTime SaleStartAt { get; set; }
        public DateTime SaleEndAt { get; set; }
        public List<TicketDetailDto> Tickets { get; set; }
    }
}