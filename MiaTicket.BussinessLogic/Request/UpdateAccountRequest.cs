using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class UpdateAccountRequest
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }

        public int Gender { get; set; }
        public IFormFile? AvatarFile { get; set; }
}
}
