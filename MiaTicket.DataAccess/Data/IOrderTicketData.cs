﻿using MiaTicket.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IOrderTicketData
    {

    }

    public class OrderTicetData: IOrderTicketData{
        private readonly MiaTicketDBContext _context;

        public OrderTicetData(MiaTicketDBContext context)
        {
            _context = context;
        }
    }
}
