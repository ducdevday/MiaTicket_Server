using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class SearchEventValidation : BaseValidation<SearchEventRequest>
    {
        public SearchEventValidation(SearchEventRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (_value.Page < 1 || _value.Size < 1 
                || (!string.IsNullOrEmpty(_value.Categories) && !RegexUtil.isSearchEventCategoriesValid(_value.Categories))
                || (!string.IsNullOrEmpty(_value.PriceRanges) && !RegexUtil.isSearchEventPriceRangesValid(_value.PriceRanges)) 
                ) {
                _message = "Invalid Request";
            }
        }
    }
}
