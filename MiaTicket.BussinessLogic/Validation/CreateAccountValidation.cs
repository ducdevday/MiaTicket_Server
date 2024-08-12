﻿using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public class CreateAccountValidation : BaseValidation<CreateAccountRequest>
    {
        public CreateAccountValidation(CreateAccountRequest input) : base(input)
        {

        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(_value.Name)
                || !RegexUtil.isStringLengthValid(_value.Name, 255)
                || string.IsNullOrEmpty(_value.Email)
                || !RegexUtil.isEmailValid(_value.Email)
                || string.IsNullOrEmpty(_value.Password)
                || !RegexUtil.isPasswordValid(_value.Password)
                || !RegexUtil.isStringLengthValid(_value.Password, 255)
                || _value.BirthDate > DateTime.UtcNow
                || !RegexUtil.isPhomeNumberValid(_value.PhoneNumber)
                ) {
                _isValid = false;
                _message = "Invalid Request";
            }
            _isValid = true;
            return;
        }
    }
}
