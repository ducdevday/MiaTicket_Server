using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsDisable { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
