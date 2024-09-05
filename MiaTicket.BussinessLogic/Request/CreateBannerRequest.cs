using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class CreateBannerRequest
    {
        public int EventId { get; set; }
        public IFormFile VideoFile { get; set; }
    }
}
