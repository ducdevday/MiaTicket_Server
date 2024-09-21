using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.VNPay.Util
{
    public class VNPayUtil
    {
        public static String HmacSHA512(string key, String inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
        public static string GetIpAddress(IHttpContextAccessor httpContextAccessor)
        {
            string ipAddress = string.Empty;

            try
            {
                ipAddress = httpContextAccessor.HttpContext?.Request?.Headers["X-Forwarded-For"].FirstOrDefault();

                if (string.IsNullOrEmpty(ipAddress) || ipAddress.ToLower() == "unknown")
                {
                    ipAddress = httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                }

                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = "Unknown IP";
                }
            }
            catch (Exception ex)
            {
                ipAddress = "Invalid IP: " + ex.Message;
            }

            return ipAddress;
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
