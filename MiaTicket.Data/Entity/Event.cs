﻿using MiaTicket.Data.Enum;

namespace MiaTicket.Data.Entity
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
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
        public EventStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Voucher>? Vouchers { get; set; }

        public BankAccount BankAccount { get; set; }
        public Banner? Banner { get; set; }
        public List<ShowTime> ShowTimes { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public List<EventOrganizer> EventOrganizers { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
