using MiaTicket.BussinessLogic.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class UpdateEventRequest
    {
        public string Name { get; set; }
        public bool IsOffline { get; set; }
        public string AddressName { get; set; }
        public string AddressNo { get; set; }
        public string AddressWard { get; set; }
        public string AddressDistrict { get; set; }
        public string AddressProvince { get; set; }
        public IFormFile? BackgroundFile { get; set; }
        public IFormFile? LogoFile { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerInformation { get; set; }
        public IFormFile? OrganizerLogoFile { get; set; }
        public string PaymentAccount { get; set; }
        public string PaymentNumber { get; set; }
        public string PaymentBankName { get; set; }
        public string PaymentBankBranch { get; set; }
        public int CategoryId { get; set; }
        public Guid UserId { get; set; }
        public List<ShowTimeDto> ShowTimes { get; set; }
    }
}
