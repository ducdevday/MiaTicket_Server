using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Util
{
    public class FormaterUtil
    {
        /// <summary>
        /// Categories Id Pattern: 12,34,56
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<int> SearchEventCategories(string input) { 
            List<string> cates = input.Split(',').ToList();
            List<int> result = [];
            foreach (string category in cates)
            {
                if (int.TryParse(category, out int parsedInt))
                {
                    result.Add(parsedInt);
                }
            }
            return result;
        }
        /// <summary>
        /// Price Range Pattern: 123.3-456.6
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<double> SearchEventPriceRanges(string input) {
            List<string> priceRanges = input.Split('-').ToList();
            List<double> result = [];
            foreach (string price in priceRanges)
            {
                if (double.TryParse(price, out double parsedDouble))
                {
                    result.Add(parsedDouble);
                }
            }
            return result;
        }
    }
}
