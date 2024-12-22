using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Helpers
{
    public static class PaymentUtils
    {
        public static string FormattedDate(string value, string lastValidDate)
        {
            const int index = 2;

            if (value.Length < lastValidDate.Length && value.EndsWith("/"))
            {
                value = value.Substring(0, value.Length - 1);
            }
            else if (value.Length > lastValidDate.Length && !value.Contains("/"))
            {
                switch (value.Length)
                {
                    case 2:
                        value += "/";
                        break;
                    case 3:
                        value = value.Substring(0, index) + "/" + value.Substring(index);
                        break;
                }
            }

            return value;

        }

        public static string FormatCreditCardNumber(string value)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                if (i > 0 && i % 4 == 0)
                    builder.Append(' ');
                builder.Append(value[i]);
            }
            return builder.ToString();
        }
    }
}
