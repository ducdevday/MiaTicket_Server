using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Util
{
    public class PasswordUtil
    {
        public static byte[] HashPassword(string value)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(value);
            byte[] hashedBytes = Encoding.UTF8.GetBytes(hashedPassword);
            return hashedBytes;
        }


        public static bool ValidatePassword(string raw, byte[] hashedBytes)
        {
            string hashedPassword = Encoding.UTF8.GetString(hashedBytes);
            return BCrypt.Net.BCrypt.Verify(raw, hashedPassword);
        }
    }
}
