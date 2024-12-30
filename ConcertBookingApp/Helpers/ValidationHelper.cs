using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConcertBookingApp.Helpers
{
    public static class ValidationHelper
    {
        private static readonly Dictionary<string, Regex> RegexPatterns = new Dictionary<string, Regex>
        {
            { "Email", new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$") },
            { "Name", new Regex(@"^[a-z\s]+$", RegexOptions.IgnoreCase) },
            { "Maestro", new Regex(@"^(50|5[6-9]|6[0-9])\d{10,17}$") },
            { "Mastercard", new Regex(@"^(5[1-5])\d{14}$") },
            { "Visa", new Regex(@"^(4)\d{11,17}$") },
            { "Expire", new Regex(@"^[0-9]\d{0,3}$")}
        };

        public static bool ValidateInput(string input, string fieldType, string lastValidValue, out string validatedValue)
        {
            string formatted = input.Replace(" ", "");
            if (RegexPatterns.ContainsKey(fieldType) && RegexPatterns[fieldType].IsMatch(formatted))
            {
                validatedValue = input;
                return true;
            }
           
            validatedValue = lastValidValue;
            return false;
        }

        public static string GetCardType(string value)
        {
            var formatString = value.Replace(" ", "");
            return RegexPatterns.FirstOrDefault(x => x.Key != "Email" && x.Key != "Name" && x.Key != "Expire" && x.Value.IsMatch(formatString)).Key ?? string.Empty;

        }
        public static bool IsValidExpire(string value, DateTime currentDate, out DateTime parsedDate)
        {
            parsedDate = DateTime.MinValue;
            return value.Length == 5 &&
                   DateTime.TryParseExact(value, "MM/yy", null, System.Globalization.DateTimeStyles.None,
                       out parsedDate) &&
                   parsedDate >= currentDate &&
                   parsedDate <= currentDate.AddYears(10);
        }
    }
}
