using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.ZaloPay.Util
{
    public class ZaloPayUtil
    {
        public static String HmacSHA256(string inputData, string key)
        {
            byte[] keyByte = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(inputData);
            byte[] hashMessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
            return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
        }
        public static string GetRandomNumber(int length)
        {
            Random rnd = new Random();
            const string chars = "0123456789";
            StringBuilder sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[rnd.Next(chars.Length)]);
            }

            return sb.ToString();
        }
    }
}
