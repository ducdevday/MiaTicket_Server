using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;


namespace MiaTicket.BussinessLogic.Validation
{
    public class SearchVoucherValidation : BaseValidation<SearchVoucherRequest>
    {
        public SearchVoucherValidation(SearchVoucherRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_value.Code) || !RegexUtil.isStringLengthValid(_value.Code, 12)) {
                _message =  "Invalid Request";
            }
        }
    }
}
