using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class UpdatePaymentVnPayRequest
    {
        public string TransactionCode { get; set; }
        public string TransactionDate { get; set; }
    }
}
