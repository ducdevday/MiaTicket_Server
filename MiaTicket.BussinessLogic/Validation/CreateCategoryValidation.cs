using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class CreateCategoryValidation : BaseValidation<CreateCategoryRequest>
    {
        public CreateCategoryValidation(CreateCategoryRequest input) : base(input)
        {
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_value.Name) || !RegexUtil.isStringLengthValid(_value.Name, 255)) {
                _message = "Invalid Input";
            }
        }
    }
}
