using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParvazPardaz.Common.Extension
{
    public static class CurrencyExtension
    {
        /// <summary>
        /// amount.FormatCurrency("AUD");  ===> $100.00
        /// amount.FormatCurrency("USD");  ===> $100.00
        /// amount.FormatCurrency("GBP");  ===> £100.00
        /// amount.FormatCurrency("EUR");  ===> 100,00 €
        /// amount.FormatCurrency("VND");  ===> 100,00 ₫
        // /amount.FormatCurrency("IRN");  ===> ₹ 100.00
        // /amount.FormatCurrency("IRR");  ===> تومان 100.00
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currencyCode"></param>
        /// <returns></returns>
        public static string FormatCurrency(this decimal amount, string currencyCode)
        {
            var culture = (from c in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                           let r = new RegionInfo(c.LCID)
                           where r != null
                           && r.ISOCurrencySymbol.ToUpper() == currencyCode.ToUpper()
                           select c).FirstOrDefault();

            if (culture == null)
                return amount.ToString();

            return string.Format(culture, "{0:C0}", amount);
        }

        public static string FormatCurrency(this long amount, string currencyCode)
        {
            var culture = (from c in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                           let r = new RegionInfo(c.LCID)
                           where r != null
                           && r.ISOCurrencySymbol.ToUpper() == currencyCode.ToUpper()
                           select c).FirstOrDefault();

            if (culture == null)
                return amount.ToString("0.00");

            return string.Format(culture, "{0:C}", amount);
        }
    }
}
