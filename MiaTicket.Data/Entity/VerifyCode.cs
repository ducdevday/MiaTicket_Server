using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class VerifyCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime ExpireAt { get; set; }
        public bool IsUsed { get; set; }
        public VerifyType Type {  get; set; }  
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
