using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Email.Model
{
    public class ResetPasswordEmail : IEmailModel
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Body { set; get; }
        public string Subject { get; set; }
    }
}
