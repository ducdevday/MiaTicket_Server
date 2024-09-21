using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class CreateEventValidation : BaseValidation<CreateEventRequest>
    {
        public CreateEventValidation(CreateEventRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_value.Name)
                || !RegexUtil.isStringLengthValid(_value.Name, 255)
                || (string.IsNullOrEmpty(_value.AddressName)
                || !RegexUtil.isStringLengthValid(_value.AddressName, 255)
                || string.IsNullOrEmpty(_value.AddressNo)
                || !RegexUtil.isStringLengthValid(_value.AddressNo, 255)
                || string.IsNullOrEmpty(_value.AddressWard)
                || !RegexUtil.isStringLengthValid(_value.AddressWard, 255)
                || string.IsNullOrEmpty(_value.AddressDistrict)
                || !RegexUtil.isStringLengthValid(_value.AddressDistrict, 255)
                || string.IsNullOrEmpty(_value.AddressProvince)
                || !RegexUtil.isStringLengthValid(_value.AddressProvince, 255)
                || (_value.BackgroundFile == null)
                || (_value.LogoFile == null)
                || string.IsNullOrEmpty(_value.OrganizerName)
                || !RegexUtil.isStringLengthValid(_value.OrganizerName, 255)
                || string.IsNullOrEmpty(_value.OrganizerInformation)
                || !RegexUtil.isStringLengthValid(_value.OrganizerInformation, 255)
                || (_value.OrganizerLogoFile == null)
                || string.IsNullOrEmpty(_value.PaymentAccount)
                || !RegexUtil.isStringLengthValid(_value.PaymentAccount, 100)
                || string.IsNullOrEmpty(_value.PaymentNumber)
                || !RegexUtil.isStringLengthValid(_value.PaymentNumber, 50)
                || string.IsNullOrEmpty(_value.PaymentBankName)
                || !RegexUtil.isStringLengthValid(_value.PaymentBankName, 50)
                || string.IsNullOrEmpty(_value.PaymentBankBranch)
                || !RegexUtil.isStringLengthValid(_value.PaymentBankBranch, 50)
                || _value.ShowTimes == null
                || _value.ShowTimes.Count == 0
                || _value.ShowTimes.FirstOrDefault(x => !ValidateShowTime(x)) == null)
                )
            {
                _message = "Invalid Request";
            }
            return;
        }

        public bool ValidateShowTime(ShowTimeDto showTime)
        {
            if (showTime.ShowStartAt < DateTime.Now || showTime.ShowEndAt < DateTime.Now || showTime.ShowStartAt <= showTime.ShowEndAt
                || showTime.SaleStartAt < DateTime.Now || showTime.SaleStartAt < DateTime.Now || showTime.SaleStartAt < showTime.SaleEndAt
                || showTime.Tickets == null || showTime.Tickets.Count == 0 || showTime.Tickets.FirstOrDefault(x => !ValidateTicket(x)) != null)
            {
                return false;
            }
            return true;
        }

        public bool ValidateTicket(TicketDto ticket)
        {
            if (string.IsNullOrEmpty(ticket.Name)
                || !RegexUtil.isStringLengthValid(ticket.Name, 255)
                || ticket.MaximumPurchase < ticket.MinimumPurchase
                || (!string.IsNullOrEmpty(ticket.Description) && !RegexUtil.isStringLengthValid(ticket.Description, 50))
                )
            {
                return false;
            }
            return true;
        }
    }
}
