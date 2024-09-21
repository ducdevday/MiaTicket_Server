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
        /// <summary>
        /// Price Pattern: 100.000đ
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static string FormatPrice(double price)
        {
            return string.Format("{0:n0}đ", price);
        }

        public static DateTime ConvertUtcToLocal(DateTime utcDateTime)
        {

            return utcDateTime.ToLocalTime();
        }

        public static string FormatDateTimeRange(DateTime date1, DateTime date2)
        {
            string FormatTime(DateTime date)
            {
                return date.ToString("HH:mm");
            }

            string FormatDate(DateTime date)
            {
                return date.ToString("d MMM, yyyy"); // Formats as "1 Jan, 2024"
            }

            string time1 = FormatTime(date1);
            string time2 = FormatTime(date2);
            string date1Formatted = FormatDate(date1);
            string date2Formatted = FormatDate(date2);

            // Compare if the same year
            if (date1.Year == date2.Year)
            {
                // Compare if the same month
                if (date1.Month == date2.Month)
                {
                    // Compare if the same day
                    if (date1.Day == date2.Day)
                    {
                        // Same day
                        return $"{time1} - {time2}, {date1Formatted}";
                    }

                    // Same month
                    string day1 = date1.Day.ToString();
                    string day2 = date2.Day.ToString();
                    string monthYear = date1.ToString("MMM yyyy");
                    return $"{time1}, {day1} - {time2}, {day2} {monthYear}";
                }

                // Same year
                string monthDay1 = date1.ToString("d MMM");
                string monthDay2 = date2.ToString("d MMM");
                return $"{time1}, {monthDay1} - {time2}, {monthDay2} {date1.Year}";
            }

            // Different years
            return $"{time1}, {date1Formatted} - {time2}, {date2Formatted}";
        }

    }
}
