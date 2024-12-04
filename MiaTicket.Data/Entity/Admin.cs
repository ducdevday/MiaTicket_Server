using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class Admin
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public byte[] Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsPasswordTemporary { get; set; }
    }
}
