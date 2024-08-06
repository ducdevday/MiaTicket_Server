namespace MiaTicket.Data.Entity
{
    public class Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsOffline { get; set; }
        public string? AddressName { get; set; }
        public string? AddressNo { get; set; }
        public string? AddressWard { get; set; }
        public string? AddressDistinct { get; set; }
        public string? AddressProvince { get; set; }
        public string BackgroundUrl { get; set; }
        public string LogoUrl { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerInformation { get; set; }
        public string OrganizerLogoUrl {  get; set; }
        public string PaymentAccount { get; set; }
        public string PaymentNumber { get; set; }
        public string PaymentBankName { get; set; }
        public string PaymentBankBranch { get; set; }

        public List<Voucher>? Vouchers { get; set; }

        public Banner? Banner { get; set; }
        public List<ShowTime> ShowTimes { get; set; }

        public Category Category { get; set; }
        public string CategoryId { get; set; }
        public User User { get; set; }  
        public string UserId { get; set; }
        public Order Order { get; set; }
    }
}
