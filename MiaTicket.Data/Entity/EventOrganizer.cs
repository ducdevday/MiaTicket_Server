using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class EventOrganizer
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        public Guid OrganizerId { get; set; }
        public User Organizer { get; set; } 
        public OrganizerPosition Position { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
