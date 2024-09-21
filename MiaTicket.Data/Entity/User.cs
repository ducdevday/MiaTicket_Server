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
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public byte[] Password { get; set; }
        public string PhoneNumber { get; set; }
        public UserStatus Status { get; set; }
        public Role Role { get; set; }
        public List<VerifyCode>? VerifyCodes { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }
        public List<Order>? Orders { get; set; }
        public List<Event>? Events { get; set; }
    }
}
