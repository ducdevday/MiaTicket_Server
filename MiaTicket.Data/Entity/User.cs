using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.Data.Entity
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public Order? Order { get; set; }
        public UserStatus UserStatus { get; set; }
        public List<Event>? Events { get; set; }
    }
}
