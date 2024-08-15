using MiaTicket.Data.Entity;
using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Model
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }

        public UserModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            AvatarUrl = user.AvatarUrl;
            BirthDate = user.BirthDate;
            Email = user.Email;
            Gender = user.Gender;
            PhoneNumber = user.PhoneNumber;
        }
    }
}
