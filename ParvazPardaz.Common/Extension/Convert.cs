using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Common.Extension
{
    public static class Convert
    {
        public static string ConvertToStringLongDate(this DateTime dateTime)
        {
            return String.Format("{0:D}", dateTime);
        }
        public static string ConvertToStringLongDateTime(this DateTime dateTime)
        {
            return String.Format("{0:ddd, MMM d, yyyy HH:mm}", dateTime);
        }
        public static string ConvertToStringTime(this TimeSpan timeSpan)
        {
            return string.Format("{0:00}:{1:00}", timeSpan.Hours, timeSpan.Minutes);
        }
        public static string ConvertToStringCurrency(this long currency)
        {
            return currency.FormatCurrency("EUR");
        }
        public static string ConvertToStringCurrency(this decimal currency)
        {
            return currency.FormatCurrency("EUR");
        }
        public static string ConvertToStringCurrencyWithoutSign(this decimal currency)
        {
            return String.Format("{0:N}", currency);
        }
        public static string ConvertToStringCurrencyWithoutSign(this long currency)
        {
            return String.Format("{0:N}", currency);
        }
        public static Int32 GetAge(this DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
            return (a - b) / 10000;
        }

    }
}
