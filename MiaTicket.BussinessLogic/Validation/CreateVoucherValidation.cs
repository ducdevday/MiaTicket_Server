using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;
using MiaTicket.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class CreateVoucherValidation : BaseValidation<CreateVoucherRequest>
    {
        public CreateVoucherValidation(CreateVoucherRequest input) : base(input)
        {
        }
        public override void Validate()
        {
            if (string.IsNullOrEmpty(_value.Name)
                || !RegexUtil.isStringLengthValid(_value.Name, 50)
                || string.IsNullOrEmpty(_value.Code)
                || !RegexUtil.isStringLengthValid(_value.Code, 12)
                || _value.StartDate > _value.EndDate
                || _value.Value < 0
                || (_value.Quantity != null && _value.Quantity <= 0)
                || (_value.MinQuantityPerOrder != null && _value.MinQuantityPerOrder <= 0)
                || (_value.MaxQuantityPerOrder != null && _value.MaxQuantityPerOrder <= 0)
                || (_value.MinQuantityPerOrder != null && _value.MaxQuantityPerOrder != null && _value.MaxQuantityPerOrder < _value.MinQuantityPerOrder)
                )
            {
                _message = "InValid Request";
            }
        }
    }
}
