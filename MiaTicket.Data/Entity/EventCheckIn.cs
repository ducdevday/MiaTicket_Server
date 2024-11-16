using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class EventCheckIn
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool IsCheckedIn { get; set; } 
        public DateTime? CheckedInAt { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public Guid? OrganizerId { get; set; }
        public User? Organizer { get; set; }

    }
}
