using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Email.Model
{
    public class EmailModel :IEmailModel
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
