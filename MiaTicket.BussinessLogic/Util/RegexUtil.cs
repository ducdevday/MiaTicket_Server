﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Util
{
    public class RegexUtil
    {
        public static bool isStringLengthValid(string input, int lenght) => new Regex("^.{0,RP}$".Replace("RP", lenght.ToString())).IsMatch(input);
        public static bool isEmailValid(string email) => new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$").IsMatch(email);
        public static bool isPhomeNumberValid(string phoneNumber) => new Regex("^(?:\\+84|0)(3[2-9]|5[6|8|9]|7[0|6-9]|8[1-5]|9[0-9])\\d{7}$").IsMatch(phoneNumber);
        public static bool isPasswordValid(string password) => new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&_])[A-Za-z\\d@$!%*?&_]{8,}$").IsMatch(password);
        public static bool isSearchEventCategoriesValid(string categories) => new Regex("^\\d+(,\\d+)*$").IsMatch(categories);
        public static bool isSearchEventPriceRangesValid(string priceRanges) => new Regex("^\\d+(\\.\\d+)?-\\d+(\\.\\d+)?$").IsMatch(priceRanges);
    }
}
