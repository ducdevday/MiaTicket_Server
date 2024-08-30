using Microsoft.AspNetCore.Http;

namespace MiaTicket.BussinessLogic.Model
{
    public class GetEventDataResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOffline { get; set; }
        public string AddressName { get; set; }
        public string AddressNo { get; set; }
        public string AddressWard { get; set; }
        public string AddressDistrict { get; set; }
        public string AddressProvince { get; set; }
        public string BackgroundUrl { get; set; }
        public string LogoUrl { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerInformation { get; set; }
        public string OrganizerLogoUrl { get; set; }
        public string PaymentAccount { get; set; }
        public string PaymentNumber { get; set; }
        public string PaymentBankName { get; set; }
        public string PaymentBankBranch { get; set; }
        public int CategoryId { get; set; }
        public List<ShowTimeDto> ShowTimes { get; set; }
    }
}
